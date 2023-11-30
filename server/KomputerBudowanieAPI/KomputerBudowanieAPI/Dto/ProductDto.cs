using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice> Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
    }
}
