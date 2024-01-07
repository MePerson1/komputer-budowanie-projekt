using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Helpers.Request;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcConfigurationRepository
    {
        Task<IEnumerable<PcConfiguration>> GetAllAsync();
        Task<IEnumerable<PcConfiguration>> GetAllAsyncPublic();
        Task<PagedList<PcConfiguration>> GetAllAsyncPublicPagination(PartsParams partsParams);
        Task<PagedList<PcConfiguration>> GetAllAsyncPagination(PartsParams partsParams);
        Task<IEnumerable<PcConfiguration>> GetAllAsync(string userId);
        Task<PagedList<PcConfiguration>> GetAllAsyncByUserIdPagination(string userId, PartsParams partsParams);
        Task<PcConfiguration?> GetByIdAsync(Guid id);

        Task<PcConfiguration> Create(PcConfigurationDto newConfigurationDto);

        Task<PcConfiguration> Update(Guid id, PcConfigurationDto entity);

        Task Delete(PcConfiguration entity);

        Task SaveChanges();

        Task<PcConfiguration?> GetDataFromIds(PcConfigurationDto dto, PcConfiguration pcConfiguration);
    }
}
