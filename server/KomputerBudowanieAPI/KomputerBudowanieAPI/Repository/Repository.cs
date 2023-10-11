using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace KomputerBudowanieAPI.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected KomBuildDbContext _context;
        public Repository(KomBuildDbContext context)
        {
            this._context = context;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        /*
         * Różne typy id sa
         */
        //public async Task<TEntity?> GetByIdAsync(int id)
        //{
        //    return await _context.Set<TEntity>().FindAsync(id);
        //}

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            SaveChanges();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            SaveChanges();
        }
    }
}
