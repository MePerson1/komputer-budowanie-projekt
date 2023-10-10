using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPowerSupplyRepository
    {
        ICollection<PowerSupply> GetAll();
        PowerSupply? GetById(int id);

        int Create(PowerSupply cpu);

        bool Update(int id, PowerSupply cpu);
        bool Delete(int id);
    }
}
