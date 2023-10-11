using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Case?> GetById(int id)
        {
            return await _context.Cases.SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
