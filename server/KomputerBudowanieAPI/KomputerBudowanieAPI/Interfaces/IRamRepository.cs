using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IRamRepository
    {
        ICollection<Ram> GetAll();
        Ram? GetById(int id);

        int Create(Ram cpu);

        bool Update(int id, Ram cpu);
        bool Delete(int id);
    }
}
