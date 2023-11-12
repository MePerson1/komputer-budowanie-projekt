namespace KomputerBudowanieAPI.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        /*
         * Różne typy id sa
         */
        Task<TEntity?> GetByIdAsync(int id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task SaveChanges();

    }
}
