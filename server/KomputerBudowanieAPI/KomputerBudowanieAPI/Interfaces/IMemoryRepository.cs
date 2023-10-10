using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IMemoryRepository
    {
        ICollection<Memory> GetAll();
        Memory? GetById(int id);

        int Create(Memory cpu);

        bool Update(int id, Memory cpu);
        bool Delete(int id);
    }
}
