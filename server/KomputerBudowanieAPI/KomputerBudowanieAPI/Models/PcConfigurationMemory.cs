namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationMemory
    {
        public int Id { get; set; }
        public Guid? PcConfigurationId { get; set; }
        public int? MemoryId { get; set; }
        public PcConfiguration? PcConfiguration { get; set; }
        public Memory? Memory { get; set; }
    }
}
