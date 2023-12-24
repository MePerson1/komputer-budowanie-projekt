using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityDataFilterService : ICompatibilityDataFilterService
    {
        private readonly ICompatibilityPartsService _compatibilityPartsService;

        public CompatibilityDataFilterService(ICompatibilityPartsService compatibilityPartsService) 
        {
            _compatibilityPartsService = compatibilityPartsService;
        }

        //Dobrze
        public void CpuFilter(PcConfiguration configuration, ref IEnumerable<Cpu> cpus)
        {
            if (configuration.Motherboard is not null)
            {
                cpus = cpus
                    .Where(cpu => Cpu_Motherboard(cpu, configuration.Motherboard))
                    .ToList();
            }
            if (configuration.CpuCooling is not null)
            {
                cpus = cpus
                    .Where(cpu => Cpu_CpuCooling(cpu, configuration.CpuCooling))
                    .ToList();
            }
            if (configuration.WaterCooling is not null)
            {
                cpus = cpus
                    .Where(cpu => Cpu_WaterCooling(cpu, configuration.WaterCooling))
                    .ToList();
            }
        }

        //Rams i Storages do poprawy
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
                motherboards = motherboards
                    .Where(motherboard => Case_Motherboard(configuration.Case, motherboard))
                    .ToList();
            }

            if (configuration.PcConfigurationRams is not null)
            {
                int ramsCount = 0;
                foreach (var m in configuration.PcConfigurationRams)
                {
                    ramsCount += m.Ram.ModuleCount * m.Quantity;
                }
                
                foreach (PcConfigurationRam ram in configuration.PcConfigurationRams)
                {
                    motherboards = motherboards
                        .Where(motherboard => Ram_Motherboard(ram.Ram, motherboard, ramsCount))
                        .ToList();
                }
            }
            if (configuration.PcConfigurationStorages is not null)
            {
                foreach (var disc in configuration.PcConfigurationStorages)
                {
                    motherboards = motherboards
                        .Where(motherboard => Storage_Motherboard(disc.Storage, motherboard))
                        .ToList();
                }
            }
        }

        // TODO:
        // - ram dokończyć
        public void CpuCoolingFilter(PcConfiguration configuration, ref IEnumerable<CpuCooling> cpuCoolings)
        {
            if (configuration.Cpu is not null)
            {
                cpuCoolings = cpuCoolings
                    .Where(cpuCooling => Cpu_CpuCooling(configuration.Cpu, cpuCooling))
                    .ToList();
            }
            if (configuration.Case is not null)
            {
                cpuCoolings = cpuCoolings
                    .Where(cpuCooling => Case_CpuCooling(configuration.Case, cpuCooling))
                    .ToList();
            }
            if (configuration.PcConfigurationRams is not null)
            {

            }
        }

        //Dobrze
        public void WaterCoolingFilter(PcConfiguration configuration, ref IEnumerable<WaterCooling> waterCoolings)
        {
            if (configuration.Cpu is not null)
            {
                waterCoolings = waterCoolings
                    .Where(waterCooling => Cpu_WaterCooling(configuration.Cpu, waterCooling))
                    .ToList();
            }
            if (configuration.Case is not null)
            {
                waterCoolings = waterCoolings
                    .Where(waterCooling => Case_WaterCooling(configuration.Case, waterCooling))
                    .ToList();
            }
        }


        //Poprawić
        public void RamFilter(PcConfiguration configuration, ref IEnumerable<Ram> rams)
        {
            if (configuration.Motherboard is not null)
            {
                rams = rams
                    .Where(ram => Ram_Motherboard(ram, configuration.Motherboard, 0))
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
                    .Where(graphicCard => GraphicCard_Motherboard(graphicCard, configuration.Motherboard))
                    .ToList();
            }
            if (configuration.PowerSupply is not null)
            {
                graphicCards = graphicCards
                    .Where(graphicCard => GraphicCard_PowerSupply(graphicCard, configuration.PowerSupply))
                    .ToList();
            }
        }

        // TODO:
        // - dokończyć
        public void CaseFilter(PcConfiguration configuration, ref IEnumerable<Case> cases)
        {

            if (configuration.Motherboard is not null)
            {
                cases = cases
                    .Where(c => Case_Motherboard(c, configuration.Motherboard))
                    .ToList();
            }
            if (configuration.CpuCooling is not null)
            {
                cases = cases
                    .Where(c => Case_CpuCooling(c, configuration.CpuCooling))
                    .ToList();
            }
            if (configuration.WaterCooling is not null)
            {
                cases = cases
                    .Where(pcCase => Case_WaterCooling(pcCase, configuration.WaterCooling))
                    .ToList();
            }
            if (configuration.PcConfigurationStorages is not null)
            {
                foreach (var relation in configuration.PcConfigurationStorages)
                {
                    cases = cases
                        .Where(pcCase => Case_Storages(pcCase, relation.Storage))
                        .ToList();
                }
            }
        }

        private bool Cpu_WaterCooling(Cpu cpu, WaterCooling waterCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_WaterCooling(ref toast, cpu, waterCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Cpu_CpuCooling(Cpu cpu, CpuCooling cpuCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_CpuCooling(ref toast, cpu, cpuCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Case_Motherboard(Case pcCase, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_Motherboard(ref toast, pcCase, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Case_CpuCooling(Case pcCase, CpuCooling cpuCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_CpuCooling(ref toast, pcCase, cpuCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Case_WaterCooling(Case pcCase, WaterCooling waterCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_WaterCooling(ref toast, pcCase, waterCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Cpu_Motherboard(Cpu cpu, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_Motherboard(ref toast, cpu, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Case_GraphicCard(Case pcCase, GraphicCard card)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_GraphicCard(ref toast, pcCase, card);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool GraphicCard_Motherboard(GraphicCard graphicCard, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, graphicCard, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool GraphicCard_PowerSupply(GraphicCard graphicCard, PowerSupply powerSupply)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, graphicCard, powerSupply);
            if(toast.Problems.Any()) return false;
            return true;
        }

        //TODO:
        // Dokończyć
        static private bool Storage_PowerSupply(Storage storage, PowerSupply powerSupply, int discsUsed)
        {


            return true;
        }

        //TODO:
        // Poprawić
        private bool Storage_Motherboard(Storage storage, Motherboard motherboard)
        {
            Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(motherboard.DriveConnectors);

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

        //TODO:
        // Poprawić
        private bool Case_Storages(Case pcCase, Storage storage) =>
            (storage.FormFactor == "3.5 cala" && pcCase.InternalBaysThreePointFiveInch > 0) ||
            (storage.FormFactor == "2.5 cala" && pcCase.InternalBaysTwoPointFiveInch > 0);

        //TODO:
        // Poprawić
        private bool Ram_Motherboard(Ram ram, Motherboard motherboard, int slotsCount) =>
            motherboard.MemoryStandard == ram.MemoryType
            && motherboard.MemoryConnectorType == ram.PinType
            && motherboard.MemorySlotsCount >= slotsCount;
                
    }
}
