using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Formats.Asn1;
using System.Runtime.CompilerServices;
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
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard is not null)
            {
                //CheckSupportedProcessor
                if (!configuration.Motherboard.SupportedProcessors.Contains(configuration.Cpu.Line))
                {
                    toast.Problems.Add("Motherboard doesn't support this Cpu line!");
                }
                //CheckCPUSocket
                if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
                {
                    toast.Problems.Add("Motherboard socket is different then cpu socket type!");
                }
            }

            return toast;
        }

        public async Task<Toast?> MemoryCompatibilityCheck(PcConfiguration configuration) //DO DOKOŃCZENIA!!!!!!!!!!!
        {
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }
            
            if (configuration.Motherboard is not null)
            {
                Dictionary<string, int> connectors = ExtractDiscConnectorInfoFromMotherboard(configuration.Motherboard.DriveConnectors);
                foreach(var disc in configuration.Memories)
                {
                    //W dyskach M2
                    //"interface": "PCI-E x4 Gen4 NVMe", "PCI-E x4 Gen3 NVMe",
                    // "formFactor": "M.2 2280", 

                    //W dyskach SATA
                    //"interface": "SATA III",
                    //"formFactor": "2.5 cala",
                    if(disc.Interface == "SATA III")
                    {
                        if (connectors["SATA 3"] > 0) connectors["SATA 3"] = connectors["SATA 3"]--;
                        else toast.Problems.Add("Lack of SATA 3 slots!");
                    }
                    else if(disc.FormFactor.Contains("M.2"))
                    {
                        if(connectors["M.2 slot"] > 0) connectors["M.2 slot"] = connectors["M.2 slot"]--;
                        else toast.Problems.Add("Lack of M.2 slots!");
                    }
                    else
                    {
                        toast.Problems.Add($"{disc.Name} disc has not recognized connector!");
                    }
                }
            }

            return toast;
        }

        static private Dictionary<string, int> ExtractDiscConnectorInfoFromMotherboard(string input)
        {
            // "M.2 slot x2, SATA 3 x4"
            // "M.2 slot x3, SATA 3 x6"
            // "M.2 slot x1, SATA 3 x4"

            Dictionary<string, int> result = new Dictionary<string, int>();

            // Dzielimy wejściowy string na części po przecinkach
            string[] parts = input.Split(',');

            foreach (var part in parts)
            {
                // Usuwamy białe znaki na początku i końcu każdej części
                string trimmedPart = part.Trim();

                if (trimmedPart.Contains("x"))
                {
                    // Jeśli w danej części występuje "x", to oczekujemy, że jest to w formie "M.2 slot x2"
                    string[] subparts = trimmedPart.Split('x');
                    if (subparts.Length == 2 && int.TryParse(subparts[1], out int count))
                    {
                        string connector = subparts[0].Trim();
                        if (result.ContainsKey(connector))
                        {
                            result[connector] += count;
                        }
                        else
                        {
                            result[connector] = count;
                        }
                    }
                }
                else
                {
                    // W przeciwnym przypadku traktujemy całą część jako jedno złącze
                    if (result.ContainsKey(trimmedPart))
                    {
                        result[trimmedPart]++;
                    }
                    else
                    {
                        result[trimmedPart] = 1;
                    }
                }
            }

            return result;
        }

        public Task<Toast?> RamCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new();

            var ramCount = 0;

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return Task.FromResult<Toast?>(toast);
            }

            //MotherboardRamCheck
            if (configuration.Motherboard is not null)
            {
                foreach (Ram ram in configuration.Rams)
                {
                    ramCount += ram.ModuleCount;
                    //CheckRamStandard
                    if (configuration.Motherboard.MemoryStandard != ram.MemoryType)
                    {
                        toast.Problems.Add("Motherboard doesn't support this memory standard! " + ram.Name);
                    }
                    //CheckMRamPinType
                    if (configuration.Motherboard.MemoryConnectorType != ram.PinType) //zmienić nazwe zmiennych może
                    {
                        toast.Problems.Add("Motherboard connector is different then ram pin type! " + ram.Name);
                    }
                }
                //CheckRamSlotCount
                if (configuration.Motherboard.MemorySlotsCount < ramCount)
                {
                    toast.Problems.Add("Motherboard don't have enough memory slots! Memory slots count: " + configuration.Motherboard.MemorySlotsCount);
                }

            }

            //CheckCoolingSize
            //tutaj sprawdzenie rozmiaru chłodzenia i czy ram jest lowprofile przy tym ale jeszcze nie ma cpucooling kinda
            if (configuration.CPU_Cooling is not null)
            {

                foreach (Ram ram in configuration.Rams)
                {

                }
            }

            return Task.FromResult<Toast?>(toast);

        }

        public async Task<Toast?> CaseCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard is not null)
            {
                //Problem w tym ,że obsługiwane standardy w case są zapisywane naraz kilka i po przecinku!
                if (configuration.Motherboard.BoardStandard == "ATX")
                {
                    string compatiblity = configuration.Case.Compatibility + ",";
                    if (!compatiblity.Contains(" " + configuration.Motherboard.BoardStandard + ",")) //Chyba poprawiłem
                    {
                        toast.Problems.Add("Case is not compatibility with motherboard standard!");
                    }
                }
                else
                {
                    if (!configuration.Case.Compatibility.Contains(configuration.Motherboard.BoardStandard))
                    {
                        toast.Problems.Add("Case is not compatibility with motherboard standard!");
                    }
                }

            }

            if (configuration.PowerSupply is not null) //DO ZROBIENIA!!!!!!!!!!!!!!!!!!!!
            {
                if (configuration.Case.PowerSupply is null)
                {
                    //nie tak chyba xd
                    //if(configuration.PowerSupply.FormFactor != configuration.Case.PowerSupply)
                    //{

                    //}
                }
                else
                {
                    toast.Problems.Add("Case already contains power supply!");
                }

            }

            if (configuration.GraphicCard is not null)
            {
                if (configuration.Case.MaxGPULengthCM < (configuration.GraphicCard.CardLengthMM * 10))
                {
                    toast.Problems.Add("Grahic Card is too big for that case!");
                }
            }

            if (configuration.CPU_Cooling is not null)
            {
                //w zaleznosci od jednostki height dla cpu cooling
                if (configuration.Case.MaxCoolingSystemHeightCM < configuration.CPU_Cooling.Height)
                {
                    toast.Problems.Add("Cpu Cooling is too high xd!");
                }
            }

            if (configuration.Memories is not null)
            {
                foreach (var storage in configuration.Memories)
                {
                    if (storage.FormFactor == "3.5 cala")
                    {
                        if (configuration.Case.InternalBaysThreePointFiveInch == 0)
                        {
                            toast.Problems.Add("Case doesn't include internal bays for 3.5\" storage!");
                        }
                    }
                    if (storage.FormFactor == "2.5 cala")
                    {
                        if (configuration.Case.InternalBaysTwoPointFiveInch == 0)
                        {
                            toast.Problems.Add("Case doesn't include internal bays for 2.5\" storage!");
                        }
                    }
                }
            }

            return toast;
        }

        public async Task<Toast?> GraphicCardCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();

            //"powerConnectors": "3x 8-pin",
            //"powerConnectors": "8-pin"
            //"powerConnectors": "6-pin",

            //"pciE8Pin_6Plus4": 4,
            //"pciE16Pin": 0,
            //"pciE8Pin": 0,
            //"pciE6Pin": 0,

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong!");
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
            if(configuration.Motherboard is not null)
            {
                if (!configuration.Motherboard.ExpansionSlots.Contains(configuration.GraphicCard.ConnectorType)) //TO TRZEBA ROZBUDOWAĆ Z TYMI GŁUPIMI STRINGAMI
                {
                    toast.Problems.Add("Graphic card and motherboard don't share the same connector!");
                }
            }
            if(configuration.PowerSupply is not null)
            {
                List<string> graphicCardConnectors = ExtractConnectorInfo(configuration.GraphicCard.PowerConnectors);

                int powerSupply6_plus2pin = configuration.PowerSupply.PCIE8Pin_6Plus4;
                int powerSupply6pin = configuration.PowerSupply.PCIE6Pin;
                int powerSupply8pin = configuration.PowerSupply.PCIE8Pin;
                int powerSupply16pin = configuration.PowerSupply.PCIE16Pin;
                foreach (var connector in graphicCardConnectors)
                {
                    if(connector == "16-pin")
                    {
                        if(powerSupply16pin > 0) { powerSupply16pin--; }
                        else { toast.Problems.Add("Insufficient 16-pin connectors on the power supply!"); }
                    }
                    else if(connector == "8-pin")
                    {
                        if (powerSupply8pin > 0){ powerSupply8pin--; }
                        else if(powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                        else { toast.Problems.Add("Insufficient 8-pin connectors on the power supply!"); }
                    }
                    else if(connector == "6-pin")
                    {
                        if (powerSupply6pin > 0) { powerSupply6pin--; }
                        else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                        else { toast.Problems.Add("Insufficient 6-pin connectors on the power supply!"); }
                    }
                    else
                    {
                        Toast error = new Toast();
                        error.Problems.Add("Something went wrong!");
                        return error;
                    }
                }
            }
            return toast;
        }

        static private List<string> ExtractConnectorInfo(string input)
        {
            List<string> connectors = new();

            string[] parts = input.Split('+'); // Dzielimy string na części po znaku '+'

            foreach (var part in parts)
            {
                if (part.Contains('x'))
                {
                    // Jeśli w danej części występuje "x", to oczekujemy, że jest to w formie "Nx 8-pin"
                    string[] subparts = part.Trim().Split('x');
                    if (subparts.Length == 2 && int.TryParse(subparts[0], out int count))
                    {
                        string connector = subparts[1].Trim();
                        for (int i = 0; i < count; i++)
                        {
                            connectors.Add(connector);
                        }
                    }
                }
                else
                {
                    // W przeciwnym przypadku, traktujemy całą część jako jedno złącze
                    connectors.Add(part.Trim());
                }
            }

            return connectors;
        }
    }
}

public class Toast
{
    public ICollection<string> Warnings { get; set; } = new List<string>();
    public ICollection<string> Problems { get; set; } = new List<string>();
}