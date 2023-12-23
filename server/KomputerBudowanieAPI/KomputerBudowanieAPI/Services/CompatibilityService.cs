﻿using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityService : ICompatibilityService
    {
        public async Task<Toast> CompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Cpu_Motherboard(ref toast, configuration);
            Cpu_GraphicCard(ref toast, configuration);
            Cpu_Cooling(ref toast, configuration);

            GraphicCard_Motherboard(ref toast, configuration);
            GraphicCard_PowerSupply(ref toast, configuration);

            Case_Motherboard(ref toast, configuration);
            Case_PowerSupply(ref toast, configuration);
            Case_GraphicCard(ref toast, configuration);
            Case_CpuCooling(ref toast, configuration);
            Case_Storages(ref toast, configuration);

            Ram_CpuCooling(ref toast, configuration);
            Ram_Motherboard(ref toast, configuration);

            Storage_Motherboard(ref toast, configuration);
            Storage_PowerSupply(ref toast, configuration);

            return toast;
        }

        public async Task<Toast> CpuCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Cpu_Motherboard(ref toast, configuration); //1 i 2
            Cpu_Cooling(ref toast, configuration);
            Cpu_GraphicCard(ref toast, configuration); //3

            return toast;
        }

        public async Task<Toast> MotherboardCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Cpu_Motherboard(ref toast, configuration); //1
            GraphicCard_Motherboard(ref toast, configuration); //2
            Case_Motherboard(ref toast, configuration); //4
            Ram_Motherboard(ref toast, configuration); //5 i 6
            Storage_Motherboard(ref toast, configuration); //7

            return toast;
        }

        public async Task<Toast> CpuCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            Case_CpuCooling(ref toast, configuration); //2
            Cpu_CpuCooling(ref toast, configuration);
            Ram_CpuCooling(ref toast, configuration); //4

            return toast;
        }

        public async Task<Toast> WaterCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            Case_WaterCooling(ref toast, configuration);
            Cpu_WaterCooling(ref toast, configuration);

            return toast;
        }

        public async Task<Toast> CoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            Case_CpuCooling(ref toast, configuration);
            Cpu_Cooling(ref toast, configuration);
            Ram_CpuCooling(ref toast, configuration);

            return toast;
        }

        public async Task<Toast> RamCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Ram_Motherboard(ref toast, configuration); //1, 2 i 3
            Ram_CpuCooling(ref toast, configuration); //4

            return toast;
        }

        public async Task<Toast> StorageCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Storage_Motherboard(ref toast, configuration); //1
            Storage_PowerSupply(ref toast, configuration); //2

            return toast;
        }

        public async Task<Toast> GraphicCardCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
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
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }
            if (configuration.GraphicCard is null)
            {
                toast.Problems.Add("Brak karty graficznej!");
                return toast;
            }

            Case_GraphicCard(ref toast, configuration); //1
            GraphicCard_Motherboard(ref toast, configuration); //3
            GraphicCard_PowerSupply(ref toast, configuration); //4

            return toast;
        }

        public async Task<Toast> PowerSupplyCompatibliityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            GraphicCard_PowerSupply(ref toast, configuration);
            Case_PowerSupply(ref toast, configuration);
            Storage_PowerSupply(ref toast, configuration);

            return toast;
        }

        public async Task<Toast> CaseCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            Case_Motherboard(ref toast, configuration); //1
            Case_PowerSupply(ref toast, configuration); //2 i 3
            Case_GraphicCard(ref toast, configuration); //4
            Case_CpuCooling(ref toast, configuration); //5
            Case_WaterCooling(ref toast, configuration);
            Case_Storages(ref toast, configuration); //7

            return toast;
        }

        //
        // Prywatne metody do sprawdzania kompatybilności części
        // PcPart1_PcPart2
        //

        private static void Cpu_GraphicCard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is null || configuration.GraphicCard is not null) return;

            if (configuration.Cpu.IntegratedGraphics is null)
            {
                toast.Warnings.Add("Ten procesor nie ma zintegrowanej karty graficznej! Dodaj kartę graficzną.");
            }
        }

        //Sprawdzanie ogólne chłodzenia aby ktoś nie dał chłodzenia wodnego i powietrznego jednocześnie
        private static void Cpu_Cooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.CpuCooling is not null && configuration.WaterCooling is not null)
            {
                toast.Problems.Add($"Procesor ma 2 chłodzenia! Wodne: {configuration.WaterCooling.Name} i powietrzne: {configuration.CpuCooling.Name}!");
            }

            Cpu_CpuCooling(ref toast, configuration);
            Cpu_WaterCooling(ref toast, configuration);
        }

        private static void Cpu_CpuCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is null || configuration.CpuCooling is null) return;
            //CPU:
            //"socketType": "Socket 1700",

            //CpuCooling:
            //"processorSocket": "1150/1151/1155/1156/1200, 1366, 1700, 2011/2011-3, 2066, 775, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5",



            List<string> sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(configuration.CpuCooling.ProcessorSocket);
            string socket = configuration.Cpu.SocketType.Replace("Socket ", "").Replace("(+)", "").Trim();

            if (!sockets.Contains(socket))
            {
                toast.Problems.Add($"{configuration.CpuCooling.Name} nie wspiera gniazda procesora {socket}!");
            }
        }

        private static void Cpu_WaterCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is null || configuration.WaterCooling is null) return;
            //CPU:
            //Intel:
            //"socketType": "Socket 1700",
            //AMD:
            //"socketType": "Socket AM4"

            //WaterCooling:
            //"intelCompatibility": "LGA 1150/1151/1155/1156/1200, LGA 1700, LGA 2011/2011-3, LGA 2066",
            //"amdCompatibility": "AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5",


            List<string> sockets;
            if (configuration.Cpu.Producer == "Intel")
            {
                sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(configuration.WaterCooling.IntelCompatibility);
            }
            else if (configuration.Cpu.Producer == "AMD")
            {
                sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(configuration.WaterCooling.AMDCompatibility);
            }
            else
            {
                toast.Problems.Add("Nie wykrywam poprawnie producenta procesora!");
                return;
            }

            string socket = configuration.Cpu.SocketType.Replace("Socket ", "").Replace("(+)", "").Trim();
            for (int i = 0; i < sockets.Count; i++)
            {
                sockets[i] = sockets[i].Replace("LGA ", "");
            }

            if (!sockets.Contains(socket))
            {
                toast.Problems.Add($"{configuration.WaterCooling.Name} nie wspiera gniazda procesora {socket}!");
            }

        }

        private static void Case_WaterCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is null || configuration.WaterCooling is null) return;
            //Case:
            //"panelFront": "120 mm/140 mm x2",
            //"panelRear": null,
            //"panelSide": null,
            //"panelBottom": null,
            //"panelTop": "120 mm x3/140 mm x2", // "120 mm/140 mm x3" // "120 mm/140 mm x2" // "120 mm x1"
            //"powerSupply": null,
            //"powerSupplyPower": null,

            //WaterCooling:
            //"radiatorSizeMM": 360,
            //"radiatorLengthMM": 395,
            //"radiatorWidthMM": 120,
            //"radiatorHeightMM": 28,
            //"fanCount": 3,
            //"fanDiameterMM": 120,

            string allPanels = $"{configuration.Case.PanelBottom}, {configuration.Case.PanelFront}, {configuration.Case.PanelRear}, {configuration.Case.PanelSide}, {configuration.Case.PanelTop}";

            Dictionary<String, int> fansPlacement = ExtractConnectorInfoService.ExtractFanDimensionsFromCase(allPanels);

            string key = $"{configuration.WaterCooling.FanDiameterMM} mm";
            if(fansPlacement.TryGetValue(key, out int value))
            {
                if(value > configuration.WaterCooling.FanCount) 
                {
                    toast.Problems.Add($"Chłodzenie {configuration.WaterCooling.Name} nie mieści się w obudowie {configuration.WaterCooling}!");
                }
            }
        }

        private static void Case_CpuCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is null || configuration.CpuCooling is null) return;
            
            //"maxCoolingSystemHeightCM": 17.5,
            //"heightMM": 155,
            //w zaleznosci od jednostki height dla cpu cooling
            if (configuration.Case.MaxCoolingSystemHeightCM * 10 < configuration.CpuCooling.HeightMM)
            {
                toast.Problems.Add("Chłodzenie CPU nie mieści się w obudowie!");
            }
        }

        private static void Case_Storages(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is null || configuration.PcConfigurationStorages is null) return;
            
            var internalBaysThreePointFiveInch = configuration.Case.InternalBaysThreePointFiveInch;
            var internalBaysTwoPointFiveInc = configuration.Case.InternalBaysTwoPointFiveInch;
            foreach (var relation in configuration.PcConfigurationStorages)
            {
                if (relation.Storage.FormFactor == "3.5 cala")
                {
                    if (internalBaysThreePointFiveInch == 0) toast.Problems.Add("Ta obudowa nie ma odpowiedniej ilości wnęk 3.5 cala!");
                    else internalBaysThreePointFiveInch -= relation.Quantity;
                }
                if (relation.Storage.FormFactor == "2.5 cala")
                {
                    if (internalBaysTwoPointFiveInc == 0) toast.Problems.Add("Ta obudowa nie ma odpowiedniej ilości wnęk 2.5 cala!");
                    else internalBaysTwoPointFiveInc -= relation.Quantity;
                }
            }
        }

        private static void Case_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is null || configuration.Motherboard is null) return;
            //Case:
            //Compatibylity: "ATX, Extended ATX (e-ATX), Micro ATX (uATX), Mini ITX"

            //Motherboard:
            //BoardStandard: "ATX", "Micro ATX"

            //Problem w tym ,że obsługiwane standardy w case są zapisywane naraz kilka i po przecinku!
            if (configuration.Motherboard.BoardStandard == "ATX")
            {
                string compatiblity = " " + configuration.Case.Compatibility + ",";
                if (!compatiblity.Contains(" " + configuration.Motherboard.BoardStandard + ",")) //Chyba poprawiłem
                {
                    toast.Problems.Add($"Obudowa nie jest kompatybilna z standardem płyty głównej {configuration.Motherboard.BoardStandard}!");
                }
            }
            else
            {
                if (!configuration.Case.Compatibility.Contains(configuration.Motherboard.BoardStandard))
                {
                    toast.Problems.Add($"Obudowa nie jest kompatybilna z standardem płyty głównej {configuration.Motherboard.BoardStandard}!");
                }
            }
        }

        // WARNING:
        // - brak danych aby sprawdzić czy powersupply wejdzie do obudowy
        private static void Case_PowerSupply(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Case is null || configuration.PowerSupply is null) return;

            //W przypadku obudowy (ale dotyczy to rozmiaru płyty głównej):
            //"compatibility": "ATX, Micro ATX (uATX), Mini ITX",

            //W przypadku zasilacza:
            //"formFactor": "ATX",
            //"formFactor": "ATX 3.0",
            if (configuration.Case.PowerSupply is not null) 
            {
                toast.Warnings.Add("Obudowa już zawiera zasilacz!");
            }
        }

        // TODO:
        // - dokończyć, jakoś nie wiem jeszcze jak to dobrze zrobić, być może dobrze się nie da
        private static void Ram_CpuCooling(ref Toast toast, PcConfiguration configuration)
        {
            if(configuration.PcConfigurationRams is null || configuration.CpuCooling is null) return;
     
            //CheckCoolingSize
            //tutaj sprawdzenie rozmiaru chłodzenia i czy ram jest lowprofile przy tym ale jeszcze nie ma cpucooling kinda
            foreach (var relation in configuration.PcConfigurationRams)
            {
                if(!relation.Ram.LowProfile && configuration.CpuCooling.DepthMM > 140)
                {
                    toast.Warnings.Add($"Ram {relation.Ram.Name} może nie zmieścić się pod chłodzeniem!");
                }
            }
        }

        static private void Ram_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.PcConfigurationRams is null || !configuration.PcConfigurationRams.Any() || configuration.Motherboard is null) return;
            
            var ramCount = 0;
            foreach (PcConfigurationRam relation in configuration.PcConfigurationRams)
            {
                ramCount += relation.Ram.ModuleCount * relation.Quantity;
                //CheckRamStandard
                if (configuration.Motherboard.MemoryStandard != relation.Ram.MemoryType)
                {
                    toast.Problems.Add("Płyta główna nie wspiera tego standardu pamięci ram! " + relation.Ram.Name);
                }
                //CheckMRamPinType
                if (configuration.Motherboard.MemoryConnectorType != relation.Ram.PinType) //zmienić nazwe zmiennych może
                {
                    toast.Problems.Add("Płyta główna ma inne złącze do ramu! " + relation.Ram.Name);
                }
            }
            //CheckRamSlotCount
            if (configuration.Motherboard.MemorySlotsCount < ramCount)
            {
                toast.Problems.Add($"Płyta głowna nie pomieści więcej niż {configuration.Motherboard.MemorySlotsCount} kości ram!");
            }
        }

        static private void Storage_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.PcConfigurationStorages is null || !configuration.PcConfigurationStorages.Any() || configuration.Motherboard is null) return;
            
            //connectors: "M.2 slot or Sata 3
            Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(configuration.Motherboard.DriveConnectors);

            foreach (var disc in configuration.PcConfigurationStorages)
            {
                //W dyskach M2
                //"interface": "PCI-E x4 Gen4 NVMe", "PCI-E x4 Gen3 NVMe",
                // "formFactor": "M.2 2280", 

                //W dyskach SATA
                //"interface": "SATA 3",
                //"formFactor": "2.5 cala",

                //Wszystkie dyski sata:
                if (connectors.ContainsKey(disc.Storage.Interface))
                {
                    int quantityLeft = connectors[disc.Storage.Interface] - disc.Quantity;
                    if (quantityLeft >= 0)
                    {
                        connectors[disc.Storage.Interface] = quantityLeft;
                    }
                    else
                    {
                        toast.Problems.Add($"Niewystarczająca ilość złączy {disc.Storage.Interface}!");
                    }
                }
                //Wszystkie dyski M2:
                else if (disc.Storage.FormFactor.Contains("M.2") && connectors.ContainsKey("M.2 slot"))
                {
                    int quantityLeft = connectors["M.2 slot"] - disc.Quantity;
                    if (quantityLeft >= 0)
                    { 
                        connectors["M.2 slot"] = quantityLeft; 
                    }
                    else 
                    { 
                        toast.Problems.Add($"Niewystarczająca ilość złączy M2!"); 
                    }    
                }
                //Jak nie znajdzie w ogóle takiego złącza na płycie:
                else
                {
                    toast.Problems.Add($"Płyta {configuration.Motherboard.Name} nie ma żadnego złącza {disc.Storage.Interface} dla dysku {disc.Storage.Name}!");
                }
            }
        }

        // WARNING:
        // - W dyskach nie ma informacji o tym jaki mają rodzaj zasilania
        static private void Storage_PowerSupply(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.PcConfigurationStorages is null || !configuration.PcConfigurationStorages.Any() || configuration.PowerSupply is null) return;

            int sataInPowerSupply = configuration.PowerSupply?.Sata ?? 0;
            foreach (var disc in configuration.PcConfigurationStorages)
            {
                if(disc.Storage.Interface.Contains("SATA"))
                {
                    int quantityLeft = sataInPowerSupply - disc.Quantity;
                    if (quantityLeft >= 0) 
                    { 
                        sataInPowerSupply = quantityLeft;
                    }
                    else
                    {
                        toast.Problems.Add("Niewystarczająca ilość złączy zasilania dla dysków!");
                    }
                }
            }
        }

        static private void Cpu_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.Cpu is null || configuration.Motherboard is null) return;

            //Motherboard:
            //"supportedProcessors": "Intel Celeron, Intel Core i3, Intel Core i5, Intel Core i7, Intel Core i9, Intel Pentium Gold",
            //CPU:
            //"line": "Core i9",
            //CheckSupportedProcessor
            var line = configuration.Cpu.Line;
            if (configuration.Cpu.Producer == "AMD")
            {
                line = Regex.Match(line, @"^([\w\-]+)").Value;
            }

            if (!configuration.Motherboard.SupportedProcessors.Contains(line))
            {
                toast.Problems.Add($"Płyta główna nie wspiera tej procesorów {configuration.Cpu.Line}!");
            }
            //Motherboard:
            //"cpuSocket": "Socket 1700",
            //CPU:
            //"socketType": "Socket 1700",
            //CheckCPUSocket
            if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
            {
                toast.Problems.Add($"Płyta główna ma inny typ gniazda {configuration.Motherboard.CPUSocket}, a procesor {configuration.Cpu.SocketType}!");
            }
        }

        static private void Case_GraphicCard(ref Toast toast, PcConfiguration configuration)
        {
            if(configuration.GraphicCard is null || configuration.Case is null) return;

            if (configuration.Case.MaxGPULengthCM * 10 < configuration.GraphicCard.CardLengthMM)
            {
                toast.Problems.Add("Karta graficzna jest za duża dla tej obudowy!");
            }   
        }

        // WARNING:
        // - tu by się jeszcze przydało zwrócić warning jak płyta główna ma starszy typ złącza od karty graficznej, problem jest taki że nie ma danych aby to zrobić
        static private void GraphicCard_Motherboard(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.GraphicCard is null || configuration.Motherboard is null) return;
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
                toast.Problems.Add("Płyta główna nie ma odpowiedniego złącza dla tej karty graficznej!");
            }
        }

        static private void GraphicCard_PowerSupply(ref Toast toast, PcConfiguration configuration)
        {
            if(configuration.GraphicCard is null || configuration.PowerSupply is null) return;
            
            List<string> graphicCardConnectors = ExtractConnectorInfoService.ExtractPowerConnectorsFromGraphicCard(configuration.GraphicCard.PowerConnectors);

            int powerSupply6_plus2pin = configuration.PowerSupply.PCIE8Pin_6Plus2;
            int powerSupply6pin = configuration.PowerSupply.PCIE6Pin;
            int powerSupply8pin = configuration.PowerSupply.PCIE8Pin;
            int powerSupply16pin = configuration.PowerSupply.PCIE16Pin;
            foreach (var connector in graphicCardConnectors)
            {
                if (connector == "16-pin")
                {
                    if (powerSupply16pin > 0) { powerSupply16pin--; }
                    else { toast.Problems.Add("Niewystarczająca ilość 16 pinowych złączy do karty graficznej!"); }
                }
                else if (connector == "8-pin")
                {
                    if (powerSupply8pin > 0) { powerSupply8pin--; }
                    else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                    else { toast.Problems.Add("Niewystarczająca ilość 8 pinowych złączy do karty graficznej!"); }
                }
                else if (connector == "6-pin")
                {
                    if (powerSupply6pin > 0) { powerSupply6pin--; }
                    else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                    else { toast.Problems.Add("Niewystarczająca ilość 6 pinowych złączy do karty graficznej!"); }
                }
                else
                {
                    toast.Problems.Add($"Coś poszło nie tak podczas sprawdzania złączy! ({connector})");
                }
            }
        }
    }
}
