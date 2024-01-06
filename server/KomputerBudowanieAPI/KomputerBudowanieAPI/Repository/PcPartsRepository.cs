using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Helpers.Request;
using KomputerBudowanieAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace KomputerBudowanieAPI.Repository
{
    public class PcPartsRepository<TEntity> : IPcPartsRepository<TEntity> where TEntity : class, IPart
    {
        protected KomBuildDbContext _context;
        public PcPartsRepository(KomBuildDbContext context)
        {
            this._context = context;
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncPagination(int page = 1, int pageSize = 10, string sortBy = null, string searchTerm = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Sort(sortBy).Search(searchTerm).AsQueryable();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }



        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var model = await _context.Set<TEntity>().FindAsync(id);

            return model;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }
    }
}
