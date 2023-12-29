using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.Extensions.Configuration;
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

        public void CpuFilter(PcConfiguration configuration, ref IEnumerable<Cpu> cpus)
        {
            if (configuration.Motherboard is not null)
            {
                cpus = cpus.Where(cpu => Cpu_Motherboard(cpu, configuration.Motherboard));
            }
            if (configuration.CpuCooling is not null)
            {
                cpus = cpus.Where(cpu => Cpu_CpuCooling(cpu, configuration.CpuCooling));
            }
            if (configuration.WaterCooling is not null)
            {
                cpus = cpus.Where(cpu => Cpu_WaterCooling(cpu, configuration.WaterCooling));   
            }
            cpus = cpus.ToList();
        }

        public void MotherboardFilter(PcConfiguration configuration, ref IEnumerable<Motherboard> motherboards)
        {
            if (configuration.Cpu is not null)
            {
                motherboards = motherboards.Where(motherboard => Cpu_Motherboard(configuration.Cpu, motherboard));
            }
            if (configuration.GraphicCard is not null)
            {
                motherboards = motherboards.Where(motherboard => GraphicCard_Motherboard(configuration.GraphicCard, motherboard));
            }
            if (configuration.Case is not null)
            {
                motherboards = motherboards.Where(motherboard => Case_Motherboard(configuration.Case, motherboard));
            }
            if (configuration.PcConfigurationRams is not null)
            {
                int ramsCount = RamCount(configuration.PcConfigurationRams);
                
                foreach (PcConfigurationRam ram in configuration.PcConfigurationRams)
                {
                    motherboards = motherboards.Where(motherboard => Ram_Motherboard(ram.Ram, motherboard, ramsCount));
                }
            }
            if (configuration.PcConfigurationStorages is not null)
            {
                (int m2Storages, int sataStorages) = StoragesCount(configuration.PcConfigurationStorages);

                motherboards = motherboards.Where(motherboard => Motherboard_To_Storages(motherboard, m2Storages, sataStorages));
            }
            if (configuration.WaterCooling is not null)
            {
                List<string> sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling($"{configuration.WaterCooling.IntelCompatibility}, {configuration.WaterCooling.AMDCompatibility}");
                motherboards = motherboards.Where(motherboard => Motherboard_WaterCooling(motherboard, configuration.WaterCooling, sockets));
            }
            if (configuration.CpuCooling is not null)
            {
                List<string> sockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(configuration.CpuCooling.ProcessorSocket);
                motherboards = motherboards.Where(motherboard => Motherboard_CpuCooling(motherboard, configuration.CpuCooling, sockets));
            }
            motherboards = motherboards.ToList();
        }

        public void CpuCoolingFilter(PcConfiguration configuration, ref IEnumerable<CpuCooling> cpuCoolings)
        {
            if (configuration.Cpu is not null)
            {
                cpuCoolings = cpuCoolings.Where(cpuCooling => Cpu_CpuCooling(configuration.Cpu, cpuCooling));
            }
            if (configuration.Case is not null)
            {
                cpuCoolings = cpuCoolings.Where(cpuCooling => Case_CpuCooling(configuration.Case, cpuCooling));
            }
            if (configuration.Motherboard is not null)
            {
                cpuCoolings = cpuCoolings.Where(cpuCooling => Motherboard_CpuCooling(configuration.Motherboard, cpuCooling));
            }
            cpuCoolings = cpuCoolings.ToList();
        }

        public void WaterCoolingFilter(PcConfiguration configuration, ref IEnumerable<WaterCooling> waterCoolings)
        {
            if (configuration.Cpu is not null)
            {
                waterCoolings = waterCoolings.Where(waterCooling => Cpu_WaterCooling(configuration.Cpu, waterCooling));
            }
            if (configuration.Case is not null)
            {
                waterCoolings = waterCoolings.Where(waterCooling => Case_WaterCooling(configuration.Case, waterCooling));
            }
            if (configuration.Motherboard is not null)
            {
                waterCoolings = waterCoolings.Where(waterCooling => Motherboard_WaterCooling(configuration.Motherboard, waterCooling));
            }
            waterCoolings = waterCoolings.ToList();
        }

        public void RamFilter(PcConfiguration configuration, ref IEnumerable<Ram> rams)
        {
            if (configuration.Motherboard is not null)
            {
                rams = rams.Where(ram => Ram_Motherboard(ram, configuration.Motherboard));
            }
            rams = rams.ToList();
        }

        public void StorageFilter(PcConfiguration configuration, ref IEnumerable<Storage> storages)
        {
            int m2Count = 0, sataCount = 0;
            if (configuration.PcConfigurationStorages is not null)
            {
                (m2Count, sataCount) = StoragesCount(configuration.PcConfigurationStorages);
            }
            if (configuration.Motherboard is not null)
            {
                Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(configuration.Motherboard.DriveConnectors);
                storages = storages.Where(storage => Storage_To_Motherboard(storage, connectors, m2Count, sataCount));
            }
            if(configuration.PowerSupply is not null)
            {
                if (sataCount <= configuration.PowerSupply.Sata)
                {
                    storages = storages.Where(storage => !storage.Interface.Contains("SATA"));
                }
            }
            storages = storages.ToList();
        }

        public void PowerSupplyFilter(PcConfiguration configuration, ref IEnumerable<PowerSupply> powerSupplies)
        {
            if (configuration.GraphicCard is not null)
            {
                powerSupplies = powerSupplies.Where(powerSupply => GraphicCard_PowerSupply(configuration.GraphicCard, powerSupply));
            }
            if (configuration.PcConfigurationStorages is not null)
            {
                int sataCount = StoragesSataCount(configuration.PcConfigurationStorages);

                powerSupplies = powerSupplies.Where(powerSupply => powerSupply.Sata >= sataCount);
            }
            powerSupplies = powerSupplies.ToList();
        }

        public void GraphicCardFilter(PcConfiguration configuration, ref IEnumerable<GraphicCard> graphicCards)
        {
            if (configuration.Case is not null)
            {
                graphicCards = graphicCards.Where(graphicCard => Case_GraphicCard(configuration.Case, graphicCard));
            }
            if (configuration.Motherboard is not null)
            {
                graphicCards = graphicCards.Where(graphicCard => GraphicCard_Motherboard(graphicCard, configuration.Motherboard));
            }
            if (configuration.PowerSupply is not null)
            {
                graphicCards = graphicCards.Where(graphicCard => GraphicCard_PowerSupply(graphicCard, configuration.PowerSupply));
            }
            graphicCards = graphicCards.ToList();
        }

        public void CaseFilter(PcConfiguration configuration, ref IEnumerable<Case> cases)
        {

            if (configuration.Motherboard is not null)
            {
                cases = cases.Where(c => Case_Motherboard(c, configuration.Motherboard));
            }
            if (configuration.CpuCooling is not null)
            {
                cases = cases.Where(c => Case_CpuCooling(c, configuration.CpuCooling));
            }
            if (configuration.WaterCooling is not null)
            {
                cases = cases.Where(pcCase => Case_WaterCooling(pcCase, configuration.WaterCooling));
            }
            if (configuration.GraphicCard is not null)
            {
                cases = cases.Where(pcCase => Case_GraphicCard(pcCase, configuration.GraphicCard));
            }
            if (configuration.PcConfigurationStorages is not null)
            {
                (int size2_5, int size3_5) = StoragesSizeCount(configuration.PcConfigurationStorages);
                cases = cases.Where(pcCase => pcCase.InternalBaysTwoPointFiveInch >= size2_5 && pcCase.InternalBaysThreePointFiveInch >= size3_5);
            }
            cases = cases.ToList();
        }

        private int StoragesSataCount(ICollection<PcConfigurationStorage> storages)
        {
            int count = 0;
            foreach (var storage in storages)
            {
                if (storage.Storage.Interface.Contains("SATA"))
                {
                    count += storage.Quantity;
                }
            }
            return count;
        }

        private (int, int) StoragesCount(ICollection<PcConfigurationStorage> storages)
        {
            int m2Count = 0;
            int sataCount = 0;
            foreach (var storage in storages)
            {
                if (storage.Storage.Interface.Contains("SATA"))
                {
                    m2Count += storage.Quantity;
                }
                else if (storage.Storage.FormFactor.Contains("M.2"))
                {
                    sataCount += storage.Quantity;
                }
            }
            return (m2Count, sataCount);
        }

        private int RamCount(ICollection<PcConfigurationRam> rams)
        {
            int count = 0;
            foreach (var ram in rams)
            {
                count += ram.Ram.ModuleCount * ram.Quantity;
            }
            return count;
        }

        private (int, int) StoragesSizeCount(ICollection<PcConfigurationStorage> storages)
        {
            int count2_5 = 0;
            int count3_5 = 0;
            foreach (var storage in storages)
            {
                if (storage.Storage.FormFactor == "2.5 cala")
                {
                    count2_5 += storage.Quantity;
                }
                else if (storage.Storage.FormFactor == "3.5 cala")
                {
                    count3_5 += storage.Quantity;
                }
            }
            return (count2_5, count3_5);
        }

        private int StoragesM2Count(ICollection<PcConfigurationStorage> storages)
        {
            int count = 0;
            foreach (var storage in storages)
            {
                if (storage.Storage.FormFactor.Contains("M.2"))
                {
                    count += storage.Quantity;
                }
            }
            return count;
        }

        private bool Motherboard_WaterCooling(Motherboard motherboard, WaterCooling waterCooling, List<string>? sockets = null)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Motherboard_WaterCooling(ref toast, motherboard, waterCooling, sockets);
            if (toast.Problems.Any()) return false;
            return true;
        }

        private bool Motherboard_CpuCooling(Motherboard motherboard, CpuCooling cpuCooling, List<string>? sockets = null)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Motherboard_CpuCooling(ref toast, motherboard, cpuCooling, sockets);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy procesor i chłodzenie wodne są ze sobą kompatybilne
        private bool Cpu_WaterCooling(Cpu cpu, WaterCooling waterCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_WaterCooling(ref toast, cpu, waterCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy procesor i chłodzenie powietrzne są ze sobą kompatybilne
        private bool Cpu_CpuCooling(Cpu cpu, CpuCooling cpuCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_CpuCooling(ref toast, cpu, cpuCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy obudowa i płyta główna są ze sobą kompatybilne
        private bool Case_Motherboard(Case pcCase, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_Motherboard(ref toast, pcCase, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy obudowa i chłodzenie powietrzne są ze sobą kompatybilne
        private bool Case_CpuCooling(Case pcCase, CpuCooling cpuCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_CpuCooling(ref toast, pcCase, cpuCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy obudowa i chłodzenie wodne są ze sobą kompatybilne
        private bool Case_WaterCooling(Case pcCase, WaterCooling waterCooling)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_WaterCooling(ref toast, pcCase, waterCooling);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy procesor i płyta główna są ze sobą kompatybilne
        private bool Cpu_Motherboard(Cpu cpu, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Cpu_Motherboard(ref toast, cpu, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy obudowa i karta graficzna są ze sobą kompatybilne
        private bool Case_GraphicCard(Case pcCase, GraphicCard card)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.Case_GraphicCard(ref toast, pcCase, card);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy karta graficzna i płyta główna są ze sobą kompatybilne
        private bool GraphicCard_Motherboard(GraphicCard graphicCard, Motherboard motherboard)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, graphicCard, motherboard);
            if (toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy karta graficzna i zasilacz są ze sobą kompatybilne
        private bool GraphicCard_PowerSupply(GraphicCard graphicCard, PowerSupply powerSupply)
        {
            Toast toast = new Toast();
            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, graphicCard, powerSupply);
            if(toast.Problems.Any()) return false;
            return true;
        }

        // Sprawdza czy dysk pasuje do zasilacza
        static private bool Storage_PowerSupply(Storage storage, PowerSupply powerSupply, int sataDiscsUsed = 0)
        {
            if (storage.Interface.Contains("SATA"))
            {
                if (sataDiscsUsed >= powerSupply.Sata)
                {
                    return false;
                }
            }
            return true;
        }

        // Sprawdza czy płyta główna będzie pasowała do ilości dodanych już dysków
        private bool Motherboard_To_Storages(Motherboard motherboard, int m2Count, int sataCount)
        {
            Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(motherboard.DriveConnectors);

            if (m2Count > 0)
            {
                if (connectors.TryGetValue("M.2 slot", out int val))
                {
                    if (val < m2Count)
                    {
                        return false;
                    }
                }
            }
            if (sataCount > 0)
            {
                if (connectors.TryGetValue("SATA 3", out int val))
                {
                    if (val < sataCount)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Sprawdza czy dysk będzie pasował do dodanej już płyty głownej, uwzględniając dodane dyski
        public bool Storage_To_Motherboard(Storage storage, Dictionary<string, int> connectors, int m2Count, int sataCount)
        {
            if (connectors.ContainsKey(storage.Interface))
            {
                if (connectors[storage.Interface] < sataCount + 1)
                {
                    return false;
                }
            }
            else if (connectors.ContainsKey("M.2 slot") && storage.FormFactor.Contains("M.2"))
            {
                if (connectors["M.2 slot"] < m2Count + 1)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        // DEPRECATED:
        // Sprawdza czy pojedynczy dysk będzie pasował do płyty głównej, nie uwzględnia innych dysków
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

        // DEPRECATED:
        // Sprawdza czy pojedynczy dysk zmieści się w obudowie
        private bool Case_Storage(Case pcCase, Storage storage) =>
            (storage.FormFactor == "3.5 cala" && pcCase.InternalBaysThreePointFiveInch > 0) ||
            (storage.FormFactor == "2.5 cala" && pcCase.InternalBaysTwoPointFiveInch > 0);

        // Sprawdza czy dany zestaw pamięci ram będzie pasował do płyty głównej, uwzlędnia sloty wykorzystane przez inne pamięci ram
        private bool Ram_Motherboard(Ram ram, Motherboard motherboard, int slotsUsed = 0) =>
            motherboard.MemoryStandard == ram.MemoryType
            && motherboard.MemoryConnectorType == ram.PinType
            && motherboard.MemorySlotsCount >= slotsUsed;
                
    }
}
