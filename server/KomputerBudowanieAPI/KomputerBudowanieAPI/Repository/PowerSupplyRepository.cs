using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class PowerSupplyRepository : Repository<PowerSupply>, IPowerSupplyRepository
    {
        public PowerSupplyRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<PowerSupply?> GetById(int id)
        {
            return await _context.PowerSupplys.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
