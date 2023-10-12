using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class GraphicCardRepository : Repository<GraphicCard>, IGraphicCardRepository
    {
        public GraphicCardRepository(KomBuildDbContext context) : base(context)
        {
        }

        public async Task<GraphicCard?> GetById(int id)
        {
            return await _context.GraphicCards.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}

