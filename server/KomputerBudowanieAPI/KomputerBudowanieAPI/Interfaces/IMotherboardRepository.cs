using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IMotherboardRepository
    {
        ICollection<Motherboard> GetAll();
        Motherboard? GetById(int id);

        int Create(Motherboard cpu);

        bool Update(int id, Motherboard cpu);
        bool Delete(int id);
    }
}
