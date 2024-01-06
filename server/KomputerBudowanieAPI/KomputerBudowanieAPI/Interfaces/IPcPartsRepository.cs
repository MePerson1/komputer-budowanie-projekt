using KomputerBudowanieAPI.Helpers.Request;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcPartsRepository<TEntity> where TEntity : class, IPart
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagedList<TEntity>> GetAllAsyncPagination(PartsParams partsParams);
        Task<IEnumerable<TEntity>> GetAllAsyncSortSearch(PartsParams partsParams);

        Task<TEntity?> GetByIdAsync(int id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task SaveChanges();


    }
}
