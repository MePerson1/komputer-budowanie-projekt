using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcConfigurationRepository
    {
        ICollection<PcConfiguration> GetAll();
        PcConfiguration? GetById(Guid id);

        int Create(PcConfiguration cpu);

        bool Update(Guid id, PcConfiguration cpu);
        bool Delete(Guid id);
    }
}
