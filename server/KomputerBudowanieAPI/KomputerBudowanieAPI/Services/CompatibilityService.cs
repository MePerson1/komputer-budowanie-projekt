using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Formats.Asn1;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityService : ICompatibilityService
    {
        protected KomBuildDbContext _context;
        public CompatibilityService(KomBuildDbContext context)
        {
            this._context = context;
        }
        public async Task<string> CompatibilityCheck(PcConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard != null)
            {
                //CheckSupportedProcessor
                if (!configuration.Motherboard.SupportedProcessors.Contains(configuration.Cpu.Line))
                {
                    toast.Problems.Add("Motherboard doesn't support this Cpu line!");
                }
                //CheckCPUSocket
                if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
                {
                    toast.Problems.Add("Wrong motherboard socket!");
                }
            }

            return toast;
        }

        public async Task<Toast?> MemoryCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard != null)
            {

            }

            return toast;
        }

        public async Task<Toast?> GraphicCardCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new Toast();

            //"powerConnectors": "3x 8-pin",
            //"powerConnectors": "8-pin"
            //"powerConnectors": "6-pin",

            //"pciE8Pin_6Plus4": 4,
            //"pciE16Pin": 0,
            //"pciE8Pin": 0,
            //"pciE6Pin": 0,

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }
            if(configuration.GraphicCard is null)
            {
                toast.Problems.Add("No graphic card!");
                return toast;
            }
            if (configuration.Case is not null && configuration.Case.MaxGPULengthCM <= configuration.GraphicCard.CardLengthMM * 10)
            {
                toast.Problems.Add("Grahic Card is too big for that case!");
            }
            if(!configuration.Motherboard.ExpansionSlots.Contains(configuration.GraphicCard.ConnectorType)) //TO TRZEBA ROZBUDOWAĆ Z TYMI GŁUPIMI STRINGAMI
            {
                toast.Problems.Add("Graphic card and motherboard don't share the same connector!");
            }
            if(configuration.PowerSupply is not null)
            {
                int connectorCount;
                string connectorType1;
                string connectorType2;
                (connectorCount, connectorType1, connectorType2) =ExtractConnectorInfo(configuration.GraphicCard.PowerConnectors);

                if (connectorType1 == "8-pin" || connectorType2 == "8-pin")
                {
                    if (configuration.PowerSupply.PCIE8Pin_6Plus4 < connectorCount || configuration.PowerSupply.PCIE8Pin < connectorCount)
                    {
                        toast.Problems.Add("Insufficient 8-pin connectors on the power supply");
                    }
                }

                if (connectorType1 == "16-pin" || connectorType2 == "16-pin")
                {
                    if (configuration.PowerSupply.PCIE16Pin < connectorCount)
                    {
                        toast.Problems.Add("Insufficient 16-pin connectors on the power supply");
                    }
                }

                if (connectorType1 == "6-pin" || connectorType2 == "6-pin")
                {
                    if (configuration.PowerSupply.PCIE8Pin_6Plus4 < connectorCount || configuration.PowerSupply.PCIE6Pin < connectorCount)
                    {
                        toast.Problems.Add("Insufficient 6-pin connectors on the power supply");
                    }
                }


            }



            return toast;
        }

        static private (int, string, string) ExtractConnectorInfo(string input)
        {
            string pattern;
            bool dual = false;
            if (input.Contains("x")) //"3x 8-pin"
            {
                pattern = @"(\d+)x? (\d+-pin)";
            }
            else if (input.Contains("+")) //"6-pin + 8-pin"
            {
                pattern = @"(\d+-pin \+ \d+-pin)";
                dual = true;
            }
            else //"6-pin"
            {
                pattern = @"()(\d+-pin)";
            }

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (dual)
                {
                    string connectorType1 = match.Groups[1].Value;
                    string connectorType2 = match.Groups[2].Value;
                    return (2, connectorType1, connectorType2);
                }
                else
                {
                    string connectorCount = match.Groups[1].Value;
                    string connectorType = match.Groups[2].Value;

                    if (!string.IsNullOrEmpty(connectorCount))
                    {
                        return (int.Parse(connectorCount), connectorType, "");
                    }
                    else if (!string.IsNullOrEmpty(connectorType))
                    {
                        return (1, connectorType, "");
                    }
                }
            }

            return (0, "unknown", "unknown");
        }
    }
}

public class Toast
{
    public ICollection<string> Warnings { get; set; } = new List<string>();
    public ICollection<string> Problems { get; set; } = new List<string>();
}