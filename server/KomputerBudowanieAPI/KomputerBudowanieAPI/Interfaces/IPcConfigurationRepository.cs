using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcConfigurationRepository
    {
        Task<IEnumerable<PcConfiguration>> GetAllAsync();

        Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId);

        Task<PcConfiguration?> GetByIdAsync(Guid id);

        Task<PcConfiguration> Create(PcConfigurationDto newConfigurationDto);

        Task<PcConfiguration> Update(Guid id, PcConfigurationDto entity);

        Task Delete(PcConfiguration entity);

        Task SaveChanges();

        Task<PcConfiguration?> GetDataFromIds(PcConfigurationDto dto, PcConfiguration pcConfiguration);
    }
}
