using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICaseRepository
    {
        ICollection<Case> GetAll();
        Case? GetById(int id);

        int Create(Case cpu);

        bool Update(int id, Case cpu);
        bool Delete(int id);
    }
}
