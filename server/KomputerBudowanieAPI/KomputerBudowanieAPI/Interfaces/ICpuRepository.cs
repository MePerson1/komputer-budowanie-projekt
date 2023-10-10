using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICpuRepository
    {
        ICollection<Cpu> GetAll();
        Cpu? GetById(int id);

        int Create(Cpu cpu);

        bool Update(int id, Cpu cpu);
        bool Delete(int id);
    }
}
