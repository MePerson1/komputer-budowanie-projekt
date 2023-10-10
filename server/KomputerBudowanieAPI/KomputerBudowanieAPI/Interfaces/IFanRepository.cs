using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IFanRepository
    {
        ICollection<Fan> GetAll();
        Fan? GetById(int id);

        int Create(Fan cpu);

        bool Update(int id, Fan cpu);
        bool Delete(int id);
    }
}
