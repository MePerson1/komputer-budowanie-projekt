using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IProduct
    {
        string Name { get; set; }
        ICollection<ShopPrice>? Prices { get; set; }
        string Producer { get; set; }
    }
}
