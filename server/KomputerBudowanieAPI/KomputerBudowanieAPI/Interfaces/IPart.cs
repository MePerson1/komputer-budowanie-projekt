using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IPart
    {
        string Name { get; set; }
        ICollection<ShopPrice>? Prices { get; set; }
        string Producer { get; set; }
    }
}
