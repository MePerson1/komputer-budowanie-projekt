using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IShopPriceRepository
    {
        Task<IEnumerable<ShopPrice>> GetAllAsync();

        Task<ShopPrice?> GetByIdAsync(int id);

        Task Create(ShopPrice entity);

        Task Update(ShopPrice entity);

        Task Delete(ShopPrice entity);

        Task SaveChanges();

    }
}
