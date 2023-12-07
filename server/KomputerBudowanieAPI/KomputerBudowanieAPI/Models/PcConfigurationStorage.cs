namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationStorage
    {
        public Guid PcConfigurationsId { get; set; }
        public int StoragesId { get; set; }

        public PcConfiguration PcConfiguration { get; set; } = null!;
        public Storage Storage { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
