namespace KomputerBudowanieAPI.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        /*
         * Różne typy id sa
         */
        //Task<TEntity?> GetByIdAsync(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChanges();
    }
}
