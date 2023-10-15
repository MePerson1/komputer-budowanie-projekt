using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class PcConfigurationRepository : IPcConfigurationRepository
    {
        protected KomBuildDbContext _context;
        public PcConfigurationRepository(KomBuildDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync()
        {
            return await _context.Set<PcConfiguration>().ToListAsync();
        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId)
        {
            return await _context.Set<PcConfiguration>().Where(config => config.User.Id == userId).ToListAsync();
        }

        public async Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            return await _context.Set<PcConfiguration>().FindAsync(id);
        }

        public async Task Create(PcConfiguration entity)
        {
            await _context.Set<PcConfiguration>().AddAsync(entity);
            await SaveChanges();
        }

        public async Task Delete(PcConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(PcConfiguration entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }
    }
}
