using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class MemoryRepository : Repository<Memory>, IMemoryRepository
    {
        public MemoryRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Memory?> GetById(int id)
        {
            return await _context.Memories.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
