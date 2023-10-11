using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICpuRepository : IRepository<Cpu>
    {
        Task<Cpu?> GetById(int id);
    }
}
