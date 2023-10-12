using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IRamRepository : IRepository<Ram>
    {
        Task<Ram?> GetById(int id);

    }
}
