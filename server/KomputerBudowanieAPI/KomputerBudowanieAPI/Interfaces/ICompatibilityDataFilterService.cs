using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityDataFilterService
    {
        public void CpuFilter(PcConfiguration configuration, ref IEnumerable<Cpu> cpus);
        public void MotherboardFilter(PcConfiguration configuration, ref IEnumerable<Motherboard> motherboards);
        public void CpuCoolingFilter(PcConfiguration configuration, ref IEnumerable<CpuCooling> cpuCoolings);
        public void WaterCoolingFilter(PcConfiguration configuration, ref IEnumerable<WaterCooling> waterCoolings);
        public void RamFilter(PcConfiguration configuration, ref IEnumerable<Ram> rams);
        public void StorageFilter(PcConfiguration configuration, ref IEnumerable<Storage> storages);
        public void GraphicCardFilter(PcConfiguration configuration, ref IEnumerable<GraphicCard> graphicCards);
        public void CaseFilter(PcConfiguration configuration, ref IEnumerable<Case> cases);
        public void PowerSupplyFilter(PcConfiguration configuration, ref IEnumerable<PowerSupply> powerSupplies);
    }
}
