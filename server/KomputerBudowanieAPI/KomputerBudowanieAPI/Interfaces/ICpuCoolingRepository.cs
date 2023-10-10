using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICpuCoolingRepository
    {
        ICollection<CpuCooling> GetAll();
        CpuCooling? GetById(int id);

        int Create(CpuCooling cpu);

        bool Update(int id, CpuCooling cpu);
        bool Delete(int id);
    }
}
