namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationRam
    {
        public Guid PcConfigurationsId { get; set; }
        public int RamsId { get; set; }

        public PcConfiguration PcConfiguration { get; set; } = null!;
        public Ram Ram { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
