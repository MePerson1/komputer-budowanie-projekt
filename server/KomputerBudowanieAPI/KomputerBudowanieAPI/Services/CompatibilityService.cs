using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
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

            Cpu_Motherboard(ref toast, configuration); //1 i 2
            Cpu_GraphicCard(ref toast, configuration); //3

            return toast;
        }

        public async Task<Toast?> MotherboardCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            Cpu_Motherboard(ref toast, configuration); //1
            GraphicCard_Motherboard(ref toast, configuration); //2
            //Chipset**
            Case_Motherboard(ref toast, configuration); //4
            Ram_Motherboard(ref toast, configuration); //5 i 6
            Memory_Motherboard(ref toast, configuration); //7

            return toast;
        }

        public async Task<Toast?> CpuCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();

            Case_CpuCooling(ref toast, configuration); //2
            Ram_CpuCooling(ref toast, configuration); //4

            return toast;
        }

        public Task<Toast?> RamCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return Task.FromResult<Toast?>(toast);
            }

            Ram_Motherboard(ref toast, configuration); //1, 2 i 3
            Ram_CpuCooling(ref toast, configuration); //4

            return Task.FromResult<Toast?>(toast);
        }

        public async Task<Toast?> StorageCompatibilityCheck(PcConfiguration configuration) //DO DOKOŃCZENIA!!!!!!!!!!!
        {
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            Memory_Motherboard(ref toast, configuration); //1
            //Memory_PowerSupply //2

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
            if (configuration.GraphicCard is null)
            {
                toast.Problems.Add("No graphic card!");
                return toast;
            }

            Case_GraphicCard(ref toast, configuration); //1
            GraphicCard_Motherboard(ref toast, configuration); //3
            GraphicCard_PowerSupply(ref toast, configuration); //4

            return toast;
        }

        public async Task<Toast?> PowerSupplyCompatibliityCheck(PcConfiguration configuration)
        {
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }
            GraphicCard_PowerSupply(ref toast, configuration);
            Case_PowerSupply(ref toast, configuration);

            return toast;
        }

        public async Task<Toast?> CaseCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            Case_Motherboard(ref toast, configuration); //1
            Case_PowerSupply(ref toast, configuration); //2 i 3
            Case_GraphicCard(ref toast, configuration); //4
            Case_CpuCooling(ref toast, configuration); //5
            Case_Memories(ref toast, configuration); //7

            return toast;
        }

        //
        // Prywatne metody do sprawdzania kompatybilności części
        // PcPart1_PcPart2
        //
        private static void Cpu_GraphicCard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is null || configuration.GraphicCard is not null)
            {
                return;
            }

            if (configuration.Cpu.IntegratedGraphics is null)
            {
                toast.Problems.Add("This CPU does not have a integrated graphics!");
            }
        }


        private static void Case_Memories(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is not null && configuration.Storages is not null)
            {
                foreach (var storage in configuration.Storages)
                {
                    if (storage.FormFactor == "3.5 cala" && configuration.Case.InternalBaysThreePointFiveInch == 0)
                    {
                        toast.Problems.Add("Case doesn't include internal bays for 3.5\" storage!");
                    }
                    if (storage.FormFactor == "2.5 cala" && configuration.Case.InternalBaysTwoPointFiveInch == 0)
                    {
                        toast.Problems.Add("Case doesn't include internal bays for 2.5\" storage!");
                    }
                }
            }
        }

        private static void Case_CpuCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is not null && configuration.CPU_Cooling is not null)
            {
                //w zaleznosci od jednostki height dla cpu cooling
                if (configuration.Case.MaxCoolingSystemHeightCM < configuration.CPU_Cooling.HeightMM)
                {
                    toast.Problems.Add("Cpu Cooling is too high xd!");
                }
            }
        }

        private static void Case_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is not null && configuration.Motherboard is not null)
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
        }

        private static void Case_PowerSupply(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is not null && configuration.PowerSupply is not null)
            {
                if (configuration.Case.PowerSupply is null) //tego z danych które mamy to zrobić się nie da xd
                {
                    //W przypadku obudowy (ale dotyczy to rozmiaru płyty głównej:
                    //"compatibility": "ATX, Micro ATX (uATX), Mini ITX",

                    //W przypadku zasilacza:
                    //"formFactor": "ATX",
                    //"formFactor": "ATX 3.0",

                }
                else
                {
                    toast.Problems.Add("Case already contains power supply!");
                }
            }
        }

        private static void Ram_CpuCooling(ref Toast toast, PcConfiguration configuration) //------------------------------------------------------------------------------
        {
            if (configuration.Rams is not null && configuration.CPU_Cooling is not null)
            {
                //CheckCoolingSize
                //tutaj sprawdzenie rozmiaru chłodzenia i czy ram jest lowprofile przy tym ale jeszcze nie ma cpucooling kinda
                if (configuration.CPU_Cooling is not null)
                {

                    foreach (Ram ram in configuration.Rams)
                    {

                    }
                }
            }
        }

        static private void Ram_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Rams is not null && configuration.Motherboard is not null)
            {
                var ramCount = 0;
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
        }

        static private void Memory_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Storages is not null && configuration.Motherboard is not null)
            {
                Dictionary<string, int> connectors = ExtractDiscConnectorInfoFromMotherboard(configuration.Motherboard.DriveConnectors);
                foreach (var disc in configuration.Storages)
                {
                    //W dyskach M2
                    //"interface": "PCI-E x4 Gen4 NVMe", "PCI-E x4 Gen3 NVMe",
                    // "formFactor": "M.2 2280", 

                    //W dyskach SATA
                    //"interface": "SATA 3",
                    //"formFactor": "2.5 cala",
                    if (connectors.ContainsKey(disc.Interface))
                    {
                        if (connectors[disc.Interface] > 0) connectors[disc.Interface] = connectors[disc.Interface]--;
                        else toast.Problems.Add($"Lack of {disc.Interface} connectors!");
                    }
                    else if (disc.FormFactor.Contains("M.2"))
                    {
                        if (connectors["M.2 slot"] > 0) connectors["M.2 slot"] = connectors["M.2 slot"]--;
                        else toast.Problems.Add($"Lack of M.2 connectors!");
                    }
                    else
                    {
                        toast.Problems.Add($"{configuration.Motherboard.Name} does not have a {disc.Interface} connector for {disc.Name} disc!");
                    }
                }
            }
        }

        static private void Cpu_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is not null && configuration.Motherboard is not null)
            {
                //Motherboard:
                //"supportedProcessors": "Intel Celeron, Intel Core i3, Intel Core i5, Intel Core i7, Intel Core i9, Intel Pentium Gold",
                //CPU:
                //"line": "Core i9",
                //CheckSupportedProcessor
                if (!configuration.Motherboard.SupportedProcessors.Contains(configuration.Cpu.Line))
                {
                    toast.Problems.Add("Motherboard doesn't support this Cpu line!");
                }
                //Motherboard:
                //"cpuSocket": "Socket 1700",
                //CPU:
                //"socketType": "Socket 1700",
                //CheckCPUSocket
                if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
                {
                    toast.Problems.Add("Motherboard socket is different then cpu socket type!");
                }
            }
        }

        static private void Case_GraphicCard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.GraphicCard is not null && configuration.Case is not null)
            {
                if (configuration.Case.MaxGPULengthCM <= configuration.GraphicCard.CardLengthMM * 10)
                {
                    toast.Problems.Add("Grahic Card is too big for that case!");
                }
            }
        }

        static private void GraphicCard_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.GraphicCard is not null && configuration.Motherboard is not null)
            {
                // GraphicCard:
                // "connectorType": "PCI Express 4.0 x16",
                // "connectorType": "PCI Express 3.0 x16",
                // "connectorType": "PCI Express 4.0 x8",

                // Motherboard:
                // "expansionSlots": "PCI Express x1 (1 szt.), PCI Express x16 (3 szt.)",
                // "expansionSlots": "PCI Express x1 (3 szt.), PCI Express x16 (2 szt.)",
                string GraphicCardConnector = configuration.GraphicCard.ConnectorType;

                // Usunięcie wszystkich wersji " N.N " ze środka stringa GraphicCardConnector
                GraphicCardConnector = Regex.Replace(GraphicCardConnector, @"( \d+\.\d+ )", " ", RegexOptions.IgnoreCase).Trim();

                if (!configuration.Motherboard.ExpansionSlots.Contains(GraphicCardConnector))
                {
                    toast.Problems.Add("Graphic card and motherboard don't share the same connector!");
                }
            }
        }

        static private void GraphicCard_PowerSupply(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.GraphicCard is not null && configuration.PowerSupply is not null)
            {
                List<string> graphicCardConnectors = ExtractConnectorInfoFromGraphicCard(configuration.GraphicCard.PowerConnectors);

                int powerSupply6_plus2pin = configuration.PowerSupply.PCIE8Pin_6Plus2;
                int powerSupply6pin = configuration.PowerSupply.PCIE6Pin;
                int powerSupply8pin = configuration.PowerSupply.PCIE8Pin;
                int powerSupply16pin = configuration.PowerSupply.PCIE16Pin;
                foreach (var connector in graphicCardConnectors)
                {
                    if (connector == "16-pin")
                    {
                        if (powerSupply16pin > 0) { powerSupply16pin--; }
                        else { toast.Problems.Add("Insufficient 16-pin connectors on the power supply!"); }
                    }
                    else if (connector == "8-pin")
                    {
                        if (powerSupply8pin > 0) { powerSupply8pin--; }
                        else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                        else { toast.Problems.Add("Insufficient 8-pin connectors on the power supply!"); }
                    }
                    else if (connector == "6-pin")
                    {
                        if (powerSupply6pin > 0) { powerSupply6pin--; }
                        else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                        else { toast.Problems.Add("Insufficient 6-pin connectors on the power supply!"); }
                    }
                    else
                    {
                        toast.Problems.Add($"Something went wrong while checking connectors! ({connector})");
                    }
                }
            }
        }

        //
        // Prywatne metody do przetwarzania danych ze stringów
        //

        static private List<string> ExtractConnectorInfoFromGraphicCard(string input)
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
                        if (result.ContainsKey(connector)) { result[connector] += count; }
                        else { result[connector] = count; }
                    }
                }
                else
                {
                    // W przeciwnym przypadku traktujemy całą część jako jedno złącze
                    if (result.ContainsKey(trimmedPart)) { result[trimmedPart]++; }
                    else { result[trimmedPart] = 1; }
                }
            }

            return result;
        }
    }
}
