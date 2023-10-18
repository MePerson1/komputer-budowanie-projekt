using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcConfigurationRepository
    {
        Task<IEnumerable<PcConfiguration>> GetAllAsync();
        Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId);

        /*
         * Różne typy id sa
         */
        Task<PcConfiguration?> GetByIdAsync(Guid id);
        Task<bool> Create(PcConfigurationDto newConfigurationDto);
        Task Update(PcConfiguration entity);
        Task Delete(PcConfiguration entity);
        Task SaveChanges();
    }
}
