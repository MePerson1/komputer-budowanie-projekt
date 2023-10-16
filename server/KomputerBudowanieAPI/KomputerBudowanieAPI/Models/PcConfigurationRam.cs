namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationRam
    {
        public int Id { get; set; }
        public Guid? PcConfigurationId { get; set; }
        public int? RamId { get; set; }
        public PcConfiguration? PcConfiguration { get; set; }
        public Ram? Ram { get; set; }
    }
}
