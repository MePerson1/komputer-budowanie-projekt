using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class ShopPrice
    {
        [Key]
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Link { get; set; }
        public double Price { get; set; }

    }
}
