namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationMemory
    {
        public Guid PcConfigurationId { get; set; }
        public int MemoryId { get; set; }
        public PcConfiguration PcConfiguration { get; set; }
        public Memory Memory { get; set; }
    }
}
