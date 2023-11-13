using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityService
    {
        Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration);

        Task<Toast?> MotherboardCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> StorageCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> CpuCoolingCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> GraphicCardCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> PowerSupplyCompatibliityCheck(PcConfiguration configuration);
        Task<Toast?> RamCompatibilityCheck(PcConfiguration configuration);

        Task<Toast?> CaseCompatibilityCheck(PcConfiguration configuration);
        Task<string> CompatibilityCheck(PcConfiguration configuration);



    }
}
