using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class CpuCoolingRepository : Repository<CpuCooling>, ICpuCoolingRepository
    {
        public CpuCoolingRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<CpuCooling?> GetById(int id)
        {
            return await _context.CpuCoolings.SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
