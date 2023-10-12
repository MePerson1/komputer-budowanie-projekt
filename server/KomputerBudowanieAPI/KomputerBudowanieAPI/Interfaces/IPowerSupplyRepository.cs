using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPowerSupplyRepository : IRepository<PowerSupply>
    {

        Task<PowerSupply?> GetById(int id);

    }
}
