using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityService
    {
        Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration);
        Task<string> CompatibilityCheck(PcConfiguration configuration);

    }
}
