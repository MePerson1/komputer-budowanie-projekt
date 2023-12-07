using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityDataFilterService : ICompatibilityDataFilterService
    {
        // TODO:
        // - dodać sprawdzanie chłodzeń?
        public void CpuFilter(PcConfiguration configuration, ref IEnumerable<Cpu> cpus)
        {
            if (configuration.Motherboard is not null)
            {
                cpus = cpus
                    .Where(cpu => Cpu_Motherboard(cpu, configuration.Motherboard))
                    .ToList();
            }
        }

        public void MotherboardFilter(PcConfiguration configuration, ref IEnumerable<Motherboard> motherboards)
        {
            if (configuration.Cpu is not null)
            {
                motherboards = motherboards
                    .Where(motherboard => Cpu_Motherboard(configuration.Cpu, motherboard))
                    .ToList();
            }
            if (configuration.GraphicCard is not null)
            {
                motherboards = motherboards
                    .Where(motherboard => GraphicCard_Motherboard(configuration.GraphicCard, motherboard))
                    .ToList();
            }
            if (configuration.Case is not null)
            {
                string compatibility = configuration.Case.Compatibility + ",";
                motherboards = motherboards
                    .Where(motherboard => Case_Motherboard(configuration.Case, motherboard))
                    .ToList();
            }

            /* if (configuration.Rams is not null)
             {
                 var ramCount = 0; //tutaj jak z tym ram count to trzeba przemyslec xd
                 foreach (Ram ram in configuration.Rams)
                 {
                     motherboards = motherboards
                         .Where(motherboard => Ram_Motherboard(ram, motherboard))
                         .ToList();
                 }
             }*/
            /* if (configuration.Storages is not null)
             {
                 foreach (var disc in configuration.Storages)
                 {
                     motherboards = motherboards
                         .Where(motherboard => Storage_Motherboard(disc, motherboard))
                         .ToList();
                 }
             }*/
        }

        // TODO:
        // - dokończyć
        public void CpuCoolingFilter(PcConfiguration configuration, ref IEnumerable<CpuCooling> cpuCoolings)
        {
            if (configuration.Case is not null)
            {
                cpuCoolings = cpuCoolings
                    .Where(cpuCooling => configuration.Case.MaxCoolingSystemHeightCM < cpuCooling.HeightMM / 10)
                    .ToList();
            }

            /* if (configuration.Rams is not null)
             {

             }*/
        }

        // TODO:
        // - dokończyć
        public void WaterCoolingFilter(PcConfiguration configuration, ref IEnumerable<WaterCooling> waterCoolings)
        {
            if (configuration.Case is not null)
            {

            }

            /* if (configuration.Rams is not null)
             {

             }*/
        }

        public void RamFilter(PcConfiguration configuration, ref IEnumerable<Ram> rams)
        {
            if (configuration.Motherboard is not null)
            {
                rams = rams
                .Where(ram => Ram_Motherboard(ram, configuration.Motherboard))
                       .ToList();
            }
        }

        // TODO:
        // - dokończyć
        public void StorageFilter(PcConfiguration configuration, ref IEnumerable<Storage> storages)
        {

        }

        // TODO:
        // - dokończyć
        public void PowerSupplyFilter(PcConfiguration configuration, ref IEnumerable<PowerSupply> powerSupplies)
        {

        }

        public void GraphicCardFilter(PcConfiguration configuration, ref IEnumerable<GraphicCard> graphicCards)
        {
            if (configuration.Case is not null)
            {
                graphicCards = graphicCards
                    .Where(graphicCard => Case_GraphicCard(configuration.Case, graphicCard))
                    .ToList();
            }

            if (configuration.Motherboard is not null)
            {
                graphicCards = graphicCards
                    .Where(graphicCard => GraphicCard_Motherboard(graphicCard, configuration.Motherboard)).ToList();
            }

            if (configuration.PowerSupply is not null)
            {
                graphicCards = graphicCards
                    .Where(graphicCard => GraphicCard_PowerSupply(graphicCard, configuration.PowerSupply)).ToList();
            }
        }

        // TODO:
        // - dokończyć
        public void CaseFilter(PcConfiguration configuration, ref IEnumerable<Case> cases)
        {

            if (configuration.Motherboard is not null)
            {
                cases = cases.Where(c => Case_Motherboard(c, configuration.Motherboard)).ToList();
            }

            if (configuration.PowerSupply is not null)
            {

            }

            if (configuration.CpuCooling is not null)
            {
                cases = cases.Where(c => Case_CpuCooling(c, configuration.CpuCooling)).ToList();
            }

            /* if (configuration.Storages is not null)
             {
                 foreach (var storage in configuration.Storages)
                 {
                     cases = cases.Where(c => Case_Storages(c, storage)).ToList();
                 }
             }*/


        }

        static private bool Case_Motherboard(Case pcCase, Motherboard motherboard) =>
            (motherboard.BoardStandard == "ATX" && pcCase.Compatibility.Contains(motherboard.BoardStandard + ",")) ||
            (motherboard.BoardStandard != "ATX" && pcCase.Compatibility.Contains(motherboard.BoardStandard));
        static private bool Cpu_Motherboard(Cpu cpu, Motherboard motherboard) =>
            (cpu.Producer != "AMD" && motherboard.SupportedProcessors.Contains(cpu.Line))
            || (cpu.Producer == "AMD" && motherboard.SupportedProcessors.Contains(Regex.Match(cpu.Line, @"^([\w\-]+)").Value))
            && motherboard.CPUSocket == cpu.SocketType;

        static private bool Ram_Motherboard(Ram ram, Motherboard motherboard) =>
            motherboard.MemoryStandard == ram.MemoryType
            && motherboard.MemoryConnectorType == ram.PinType
            && motherboard.MemorySlotsCount >= ram.ModuleCount;

        static private bool Case_GraphicCard(Case pcCase, GraphicCard card) =>
            pcCase.MaxGPULengthCM * 10 >= card.CardLengthMM;

        static private bool Case_CpuCooling(Case pcCase, CpuCooling cpuCooling) =>
            (pcCase.MaxCoolingSystemHeightCM * 10) >= cpuCooling.HeightMM;

        static private bool Case_Storages(Case pcCase, Storage storage) =>
            (storage.FormFactor == "3.5 cala" && pcCase.InternalBaysThreePointFiveInch > 0) ||
            (storage.FormFactor == "2.5 cala" && pcCase.InternalBaysTwoPointFiveInch > 0);


        //pain
        static private bool GraphicCard_PowerSupply(GraphicCard graphicCard, PowerSupply powerSupply)
        {

            List<string> graphicCardConnectors = ExtractConnectorInfoService.FromGraphicCard(graphicCard.PowerConnectors);

            int powerSupply6_plus2pin = powerSupply.PCIE8Pin_6Plus2;
            int powerSupply6pin = powerSupply.PCIE6Pin;
            int powerSupply8pin = powerSupply.PCIE8Pin;
            int powerSupply16pin = powerSupply.PCIE16Pin;
            foreach (var connector in graphicCardConnectors)
            {
                if (connector == "16-pin")
                {
                    if (powerSupply16pin > 0) { powerSupply16pin--; }
                    else { return false; }
                }
                else if (connector == "8-pin")
                {
                    if (powerSupply8pin > 0) { powerSupply8pin--; }
                    else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                    else { return false; }
                }
                else if (connector == "6-pin")
                {
                    if (powerSupply6pin > 0) { powerSupply6pin--; }
                    else if (powerSupply6_plus2pin > 0) { powerSupply6_plus2pin--; }
                    else { return false; }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        static private bool GraphicCard_Motherboard(GraphicCard graphicCard, Motherboard motherboard)
        {

            // GraphicCard:
            // "connectorType": "PCI Express 4.0 x16",
            // "connectorType": "PCI Express 3.0 x16",
            // "connectorType": "PCI Express 4.0 x8",

            // Motherboard:
            // "expansionSlots": "PCI Express x1 (1 szt.), PCI Express x16 (3 szt.)",
            // "expansionSlots": "PCI Express x1 (3 szt.), PCI Express x16 (2 szt.)",
            string GraphicCardConnector = graphicCard.ConnectorType;

            // Usunięcie wszystkich wersji " N.N " ze środka stringa GraphicCardConnector
            GraphicCardConnector = Regex.Replace(GraphicCardConnector, @"( \d+\.\d+ )", " ", RegexOptions.IgnoreCase).Trim();

            if (!motherboard.ExpansionSlots.Contains(GraphicCardConnector))
            {
                return false;
            }
            return true;

        }

        static private bool Storage_Motherboard(Storage storage, Motherboard motherboard)
        {

            Dictionary<string, int> connectors = ExtractConnectorInfoService.FromMotherboard(motherboard.DriveConnectors);

            //W dyskach M2
            //"interface": "PCI-E x4 Gen4 NVMe", "PCI-E x4 Gen3 NVMe",
            // "formFactor": "M.2 2280", 

            //W dyskach SATA
            //"interface": "SATA 3",
            //"formFactor": "2.5 cala",
            if (connectors.ContainsKey(storage.Interface))
            {
                if (connectors[storage.Interface] > 0) connectors[storage.Interface] = connectors[storage.Interface]--;
                else return false;
            }
            else if (storage.FormFactor.Contains("M.2"))
            {
                if (connectors["M.2 slot"] > 0) connectors["M.2 slot"] = connectors["M.2 slot"]--;
                else return false;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
