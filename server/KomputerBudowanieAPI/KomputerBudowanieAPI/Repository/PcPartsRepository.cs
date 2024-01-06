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

        public async Task<PagedList<TEntity>> GetAllAsyncPagination(PartsParams partsParams)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Sort(partsParams.SortBy).Search(partsParams.SearchTerm).AsQueryable();

            return await PagedList<TEntity>.ToPagedList(query, partsParams.PageNumber, partsParams.PageSize);
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
