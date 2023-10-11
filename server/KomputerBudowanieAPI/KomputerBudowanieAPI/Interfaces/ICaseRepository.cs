using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICaseRepository : IRepository<Case>
    {

        Task<Case?> GetById(int id);

    }
}
