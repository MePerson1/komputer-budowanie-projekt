using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class MotherboardRepository : Repository<Motherboard>, IMotherboardRepository
    {
        public MotherboardRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<Motherboard?> GetById(int id)
        {
            return await _context.Motherboards.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
