using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICpuCoolingRepository : IRepository<CpuCooling>
    {
        Task<CpuCooling?> GetById(int id);
    }
}
