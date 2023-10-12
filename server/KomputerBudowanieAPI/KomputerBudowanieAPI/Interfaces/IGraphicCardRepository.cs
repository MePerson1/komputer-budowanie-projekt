using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IGraphicCardRepository : IRepository<GraphicCard>
    {
        Task<GraphicCard?> GetById(int id);

    }
}
