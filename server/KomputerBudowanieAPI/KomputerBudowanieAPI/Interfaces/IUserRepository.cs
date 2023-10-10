using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User? GetById(int id);

        int Create(User cpu);

        bool Update(int id, User cpu);
        bool Delete(int id);
    }
}
