using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IMemoryRepository : IRepository<Memory>
    {

        Task<Memory?> GetById(int id);


    }
}
