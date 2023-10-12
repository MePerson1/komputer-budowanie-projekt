using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IMotherboardRepository : IRepository<Motherboard>
    {

        Task<Motherboard?> GetById(int id);

    }
}
