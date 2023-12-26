using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityPartsService : ICompatibilityPartsService
    {
        /// <summary>
        /// Checking compatybility of the CPU with all coolings. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="configuration"></param>
        /// <remarks>Problems: Cooler does not support the CPU socket.</remarks>
        public void Cpu_AllCooling(ref Toast toast, PcConfiguration configuration)
        {
            if (configuration.CpuCooling is not null && configuration.WaterCooling is not null)
            {
                toast.Problems.Add($"Procesor ma 2 chłodzenia! Wodne: {configuration.WaterCooling.Name} i powietrzne: {configuration.CpuCooling.Name}!");
            }

            Cpu_CpuCooling(ref toast, configuration.Cpu, configuration.CpuCooling);
            Cpu_WaterCooling(ref toast, configuration.Cpu, configuration.WaterCooling);
        }


        /// <summary>
        /// Checking compatybility of the CPU with CpuCooling. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="cpu"></param>
        /// <param name="cpuCooling"></param>
        /// <remarks>Problems: CPU cooler does not support the CPU socket.</remarks>
        public void Cpu_CpuCooling(ref Toast toast, Cpu? cpu, CpuCooling? cpuCooling)
        {
            if (cpu is null || cpuCooling is null) return;

            List<string> sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(cpuCooling.ProcessorSocket);
            string socket = cpu.SocketType.Replace("Socket ", "").Replace("(+)", "").Trim();

            if (!sockets.Contains(socket))
            {
                toast.Problems.Add($"{cpuCooling.Name} nie wspiera gniazda procesora {socket}!");
            }
        }

        /// <summary>
        /// Checking compatybility of the CPU with water cooling. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="cpu"></param>
        /// <param name="waterCooling"></param>
        /// <remarks>Problems: Water cooler does not support the CPU socket.</remarks>
        public void Cpu_WaterCooling(ref Toast toast, Cpu? cpu, WaterCooling? waterCooling)
        {
            if (cpu is null || waterCooling is null) return;

            List<string> sockets;
            if (cpu.Producer == "Intel")
            {
                sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(waterCooling.IntelCompatibility);
            }
            else if (cpu.Producer == "AMD")
            {
                sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(waterCooling.AMDCompatibility);
            }
            else
            {
                toast.Problems.Add("Nie wykrywam poprawnie producenta procesora!");
                return;
            }

            string socket = cpu.SocketType.Replace("Socket ", "").Replace("(+)", "").Trim();
            for (int i = 0; i < sockets.Count; i++)
            {
                sockets[i] = sockets[i].Replace("LGA ", "");
            }

            if (!sockets.Contains(socket))
            {
                toast.Problems.Add($"{waterCooling.Name} nie wspiera gniazda procesora {socket}!");
            }
        }

        /// <summary>
        /// Checking compatybility of the CPU with graphic card. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="cpu"></param>
        /// <param name="graphicCard"></param>
        /// <remarks>Warnings: Prcoesor does not have integrated graphic card.</remarks>
        public void Cpu_GraphicCard(ref Toast toast, Cpu? cpu, GraphicCard? graphicCard)
        {
            if (cpu is null || graphicCard is not null) return;

            if (cpu.IntegratedGraphics is null)
            {
                toast.Warnings.Add("Ten procesor nie ma zintegrowanej karty graficznej! Dodaj kartę graficzną.");
            }
        }

        /// <summary>
        /// Checking compatybility of the case with water cooling. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="waterCooling"></param>
        /// <remarks>Problems: Water cooling does not fit into case.</remarks>
        public void Case_WaterCooling(ref Toast toast, Case? pcCase, WaterCooling? waterCooling)
        {
            if (pcCase is null || waterCooling is null) return;

            string allPanels = $"{pcCase.PanelBottom}, {pcCase.PanelFront}, {pcCase.PanelRear}, {pcCase.PanelSide}, {pcCase.PanelTop}";

            Dictionary<String, int> fansPlacement = ExtractConnectorInfoService.ExtractFanDimensionsFromCase(allPanels);

            string key = $"{waterCooling.FanDiameterMM} mm";
            if (fansPlacement.TryGetValue(key, out int value))
            {
                if (value > waterCooling.FanCount)
                {
                    toast.Problems.Add($"Chłodzenie {waterCooling.Name} nie mieści się w obudowie {pcCase.Name}!");
                }
            }
        }

        /// <summary>
        /// Checking compatybility of the case with CPU cooling. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="cpuCooling"></param>
        /// <remarks>Problems: CPU cooling does not fit into case.</remarks>
        public void Case_CpuCooling(ref Toast toast, Case? pcCase, CpuCooling? cpuCooling)
        {
            if (pcCase is null || cpuCooling is null) return;

            if (pcCase.MaxCoolingSystemHeightCM * 10 < cpuCooling.HeightMM)
            {
                toast.Problems.Add("Chłodzenie CPU nie mieści się w obudowie!");
            }
        }

        /// <summary>
        /// Checking compatybility of the case with motherboard. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="motherboard"></param>
        /// <remarks>Problems: Case is not compatible with motherboard standard.</remarks>
        public void Case_Motherboard(ref Toast toast, Case? pcCase, Motherboard? motherboard)
        {
            if (pcCase is null || motherboard is null) return;
            
            // Problem w tym ,że obsługiwane standardy w case są zapisywane naraz kilka i po przecinku!
            if (motherboard.BoardStandard == "ATX")
            {
                string compatiblity = " " + pcCase.Compatibility + ",";
                if (!compatiblity.Contains(" " + motherboard.BoardStandard + ",")) 
                {
                    toast.Problems.Add($"Obudowa nie jest kompatybilna z standardem płyty głównej {motherboard.BoardStandard}!");
                }
            }
            else
            {
                if (!pcCase.Compatibility.Contains(motherboard.BoardStandard))
                {
                    toast.Problems.Add($"Obudowa nie jest kompatybilna z standardem płyty głównej {motherboard.BoardStandard}!");
                }
            }
        }

        /// Warnings:
        /// Lack of data to check if the power supply will fit into the case.
        /// <summary>
        /// Checking compatybility of the case with power supply. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="powerSupply"></param>
        /// <remarks>Warnings: The case already contains a power supply.</remarks>
        public void Case_PowerSupply(ref Toast toast, Case? pcCase, PowerSupply? powerSupply)
        {
            if (pcCase is null || powerSupply is null) return;
            
            if (pcCase.PowerSupply is not null)
            {
                toast.Warnings.Add("Obudowa już zawiera zasilacz!");
            }
        }

        /// <summary>
        /// Checking compatybility of the CPU with motherboard. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="cpu"></param>
        /// <param name="motherboard"></param>
        /// <remarks>Problems: The motherboard does not support this processor line. | The motherboard has a different socket type than the processor.</remarks>
        public void Cpu_Motherboard(ref Toast toast, Cpu? cpu, Motherboard? motherboard)
        {
            if (cpu is null || motherboard is null) return;

            var line = cpu.Line;
            if (cpu.Producer == "AMD")
            {
                line = Regex.Match(line, @"^([\w\-]+)").Value;
            }

            if (!motherboard.SupportedProcessors.Contains(line))
            {
                toast.Problems.Add($"Płyta główna nie wspiera tej procesorów {cpu.Line}!");
            }
            
            if (motherboard.CPUSocket != cpu.SocketType)
            {
                toast.Problems.Add($"Płyta główna ma inny typ gniazda {motherboard.CPUSocket}, a procesor {cpu.SocketType}!");
            }
        }

        /// <summary>
        /// Checking compatybility of the case with graphic card. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="graphicCard"></param>
        /// <remarks> Problems: The graphics card is too big for this case.</remarks>
        public void Case_GraphicCard(ref Toast toast, Case? pcCase, GraphicCard? graphicCard)
        {
            if (graphicCard is null || pcCase is null) return;

            if (pcCase.MaxGPULengthCM * 10 < graphicCard.CardLengthMM)
            {
                toast.Problems.Add("Karta graficzna jest za duża dla tej obudowy!");
            }
        }

        /// Warnings:
        /// It would be useful to issue a warning if the motherboard has an older type of connector than the graphics card, but the problem is that there is no data to do that.
        /// <summary>
        /// Checking compatybility of the graphic card with motherboard. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="graphicCard"></param>
        /// <param name="motherboard"></param>
        /// <remarks>
        /// Problems: The motherboard does not have a suitable connector for this graphics card.
        /// </remarks>
        public void GraphicCard_Motherboard(ref Toast toast, GraphicCard? graphicCard, Motherboard? motherboard)
        {
            if (graphicCard is null || motherboard is null) return;
            
            string GraphicCardConnector = graphicCard.ConnectorType;

            // Usunięcie wszystkich wersji " N.N " (np. PCI Express 4.0 x16 to " 4.0 ") ze środka stringa GraphicCardConnector
            GraphicCardConnector = Regex.Replace(GraphicCardConnector, @"( \d+\.\d+ )", " ", RegexOptions.IgnoreCase).Trim();

            if (!motherboard.ExpansionSlots.Contains(GraphicCardConnector))
            {
                toast.Problems.Add("Płyta główna nie ma odpowiedniego złącza dla tej karty graficznej!");
            }
        }

        /// <summary>
        /// Checking compatybility of the graphic card with power supply. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="graphicCard"></param>
        /// <param name="powerSupply"></param>
        /// <remarks>
        /// Problems: Insufficient number of connectors for the graphics card.
        /// </remarks>
        public void GraphicCard_PowerSupply(ref Toast toast, GraphicCard? graphicCard, PowerSupply? powerSupply)
        {
            if (graphicCard is null || powerSupply is null) return;

            List<string> graphicCardConnectors = ExtractConnectorInfoService.ExtractPowerConnectorsFromGraphicCard(graphicCard.PowerConnectors);

            int powerSupply6_plus2pin = powerSupply.PCIE8Pin_6Plus2;
            int powerSupply6pin = powerSupply.PCIE6Pin;
            int powerSupply8pin = powerSupply.PCIE8Pin;
            int powerSupply16pin = powerSupply.PCIE16Pin;
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

        /// Warning:
        /// There is no information about the power connectors in the disc drives.
        /// <summary>
        /// Checking compatybility of the power supply with all storages. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="powerSupply"></param>
        /// <param name="storages"></param>
        /// <remarks> Problems: Insufficient power connectors for drives.</remarks>
        public void PowerSupply_Storages(ref Toast toast, PowerSupply? powerSupply, ICollection<PcConfigurationStorage>? storages)
        {
            if (storages is null || !storages.Any() || powerSupply is null) return;

            int sataInPowerSupply = powerSupply?.Sata ?? 0;
            foreach (var disc in storages)
            {
                if (disc.Storage.Interface.Contains("SATA"))
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

        /// <summary>
        /// Checking compatybility of the motherboard with all storages. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="motherboard"></param>
        /// <param name="storages"></param>
        /// <remarks> Problems: Insufficient number of connectors for storages.</remarks>
        public void Motherboard_Storages(ref Toast toast, Motherboard? motherboard, ICollection<PcConfigurationStorage>? storages)
        {
            if (storages is null || !storages.Any() || motherboard is null) return;

            //connectors: "M.2 slot or Sata 3
            Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(motherboard.DriveConnectors);

            foreach (var disc in storages)
            {
                //All sata, ata and other simillar storages:
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
                //All M2 storages:
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
                else
                {
                    toast.Problems.Add($"Płyta {motherboard.Name} nie ma żadnego złącza {disc.Storage.Interface} dla dysku {disc.Storage.Name}!");
                }
            }
        }

        /// <summary>
        /// Checking compatybility of the case with all storages. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="pcCase"></param>
        /// <param name="storages"></param>
        /// <remarks> Problems: This case does not have the appropriate number of x.x-inch bays.</remarks>
        public void Case_Storages(ref Toast toast, Case? pcCase, ICollection<PcConfigurationStorage>? storages)
        {
            if (storages is null || !storages.Any() || pcCase is null) return;

            var internalBaysThreePointFiveInch = pcCase.InternalBaysThreePointFiveInch;
            var internalBaysTwoPointFiveInc = pcCase.InternalBaysTwoPointFiveInch;
            foreach (var relation in storages)
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

        /// <summary>
        /// Checking compatybility of the motherboard with all rams. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="motherboard"></param>
        /// <param name="rams"></param>
        /// <remarks> Problems: The motherboard does not support this RAM standard. | The motherboard has a different ram connector.</remarks>
        public void Motherboard_Rams(ref Toast toast, Motherboard? motherboard, ICollection<PcConfigurationRam>? rams)
        {
            if (rams is null || !rams.Any() || motherboard is null) return;

            var ramCount = 0;
            foreach (PcConfigurationRam relation in rams)
            {
                ramCount += relation.Ram.ModuleCount * relation.Quantity;
                //CheckRamStandard
                if (motherboard.MemoryStandard != relation.Ram.MemoryType)
                {
                    toast.Problems.Add("Płyta główna nie wspiera tego standardu pamięci ram! " + relation.Ram.Name);
                }
                //CheckMRamPinType
                if (motherboard.MemoryConnectorType != relation.Ram.PinType)
                {
                    toast.Problems.Add("Płyta główna ma inne złącze do ramu! " + relation.Ram.Name);
                }
            }
            //CheckRamSlotCount
            if (motherboard.MemorySlotsCount < ramCount)
            {
                toast.Problems.Add($"Płyta głowna nie pomieści więcej niż {motherboard.MemorySlotsCount} kości ram!");
            }
        }

        /// <summary>
        /// Checking compatybility of the CPU cooling with all rams. All issiues found are added to the toast.
        /// </summary>
        /// <param name="toast"></param>
        /// <param name="cpuCooling"></param>
        /// <param name="rams"></param>
        /// <remarks> Warnings: Ram may not fit under the cooler.</remarks>
        public void CpuCooling_Rams(ref Toast toast, CpuCooling? cpuCooling, ICollection<PcConfigurationRam>? rams)
        {
            if (rams is null || !rams.Any() || cpuCooling is null) return;

            //CheckCoolingSize
            foreach (var relation in rams)
            {
                if (!relation.Ram.LowProfile && cpuCooling.DepthMM > 140)
                {
                    toast.Warnings.Add($"Ram {relation.Ram.Name} może nie zmieścić się pod chłodzeniem!");
                }
            }
        }

    }
}
