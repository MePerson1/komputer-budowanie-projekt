using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class ShopPriceRepository : IShopPriceRepository
    {
        protected KomBuildDbContext _context;
        public ShopPriceRepository(KomBuildDbContext context)
        {
            this._context = context;
        }
        public async Task Create(ShopPrice entity)
        {
            await _context.ShopPrices.AddAsync(entity);
            await SaveChanges();
        }

        public async Task Delete(ShopPrice entity)
        {
            _context.ShopPrices.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<ShopPrice>> GetAllAsync()
        {
            return await _context.ShopPrices.ToListAsync();
        }

        public async Task<ShopPrice?> GetByIdAsync(int id)
        {
            var model = await _context.ShopPrices.FindAsync(id);

            return model;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(ShopPrice entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }
    }
}
