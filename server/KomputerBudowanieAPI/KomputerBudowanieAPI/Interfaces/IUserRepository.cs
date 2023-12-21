using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetAll();
        ApplicationUser? GetById(int id);

        int Create(ApplicationUser cpu);

        bool Update(int id, ApplicationUser cpu);
        bool Delete(int id);
    }
}
