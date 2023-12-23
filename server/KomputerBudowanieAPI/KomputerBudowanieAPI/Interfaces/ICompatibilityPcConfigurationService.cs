using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityPcConfigurationService
    {
        Task<Toast> CompatibilityCheck(PcConfiguration configuration);

        Task<Toast> CpuCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> MotherboardCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> StorageCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> CpuCoolingCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> WaterCoolingCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> CoolingCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> GraphicCardCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> PowerSupplyCompatibliityCheck(PcConfiguration configuration);

        Task<Toast> RamCompatibilityCheck(PcConfiguration configuration);

        Task<Toast> CaseCompatibilityCheck(PcConfiguration configuration);
    }
}
