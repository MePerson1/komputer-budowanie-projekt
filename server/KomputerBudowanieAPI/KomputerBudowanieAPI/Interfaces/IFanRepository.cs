using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IFanRepository : IRepository<Fan>
    {
        Task<Fan?> GetById(int id);
    }
}
