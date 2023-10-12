using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class RamRepository : Repository<Ram>, IRamRepository
    {
        public RamRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Ram?> GetById(int id)
        {
            return await _context.Rams.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
