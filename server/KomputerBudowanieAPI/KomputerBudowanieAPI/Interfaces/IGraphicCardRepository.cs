using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IGraphicCardRepository
    {
        ICollection<GraphicCard> GetAll();
        GraphicCard? GetById(int id);

        int Create(GraphicCard cpu);

        bool Update(int id, GraphicCard cpu);
        bool Delete(int id);
    }
}
