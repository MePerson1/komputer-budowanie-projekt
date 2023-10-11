using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class CpuRepository : Repository<Cpu>, ICpuRepository
    {
        public CpuRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Cpu?> GetById(int id)
        {
            return await _context.Cpus.SingleOrDefaultAsync(c => c.Id == id);
        }


    }
}
