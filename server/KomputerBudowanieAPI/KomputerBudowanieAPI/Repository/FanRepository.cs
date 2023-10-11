using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class FanRepository : Repository<Fan>, IFanRepository
    {
        public FanRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Fan?> GetById(int id)
        {
            return await _context.Fans.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
