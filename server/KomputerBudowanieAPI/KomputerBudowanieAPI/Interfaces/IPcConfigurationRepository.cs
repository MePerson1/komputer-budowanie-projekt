using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcConfigurationRepository //: IRepository<PcConfiguration>
    {
        Task<ICollection<PcConfiguration>> GetAll();
        Task<ICollection<PcConfiguration>> GetAll(int userId);
        Task<PcConfiguration?> GetById(Guid id);

        int Create(PcConfiguration cpu);

        bool Update(Guid id, PcConfiguration cpu);
        bool Delete(Guid id);
    }
}
