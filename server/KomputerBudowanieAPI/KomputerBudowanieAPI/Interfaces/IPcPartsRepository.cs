namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPcPartsRepository<TEntity> where TEntity : class, IProduct
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 10, string sortBy = null, string searchTerm = null);

        Task<TEntity?> GetByIdAsync(int id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task SaveChanges();


    }
}
