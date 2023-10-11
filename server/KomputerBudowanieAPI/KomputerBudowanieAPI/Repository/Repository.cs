using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace KomputerBudowanieAPI.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private KomBuildDbContext _context;
        public Repository(KomBuildDbContext context)
        {
            this._context = context;
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
