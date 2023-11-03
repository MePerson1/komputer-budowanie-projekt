using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class GraphicCard
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string ChipsetProducer { get; set; }
        public string ChipsetType { get; set; }
        public int CoreClockMHz { get; set; }
        public int BoostClockMHz { get; set; }
        public int StreamProcessors { get; set; }
        public int ROPUnits { get; set; }
        public int TextureUnits { get; set; }
        public int RTCores { get; set; }
        public int TensorCores { get; set; }
        public bool HasDLSS3Support { get; set; }
        public string ConnectorType { get; set; }
        public int CardLengthMM { get; set; }
        public string? CardLinking { get; set; }
        public string Resolution { get; set; }
        public int RecommendedPSUCapacityW { get; set; }
        public bool HasLEDLighting { get; set; }
        public int MemorySizeGB { get; set; }
        public string MemoryType { get; set; }
        public int MemoryBusWidthBits { get; set; }
        public int MemoryClockMHz { get; set; }
        public string CoolingType { get; set; }
        public int FanCount { get; set; }
        public int DSub { get; set; }
        public int DisplayPortCount { get; set; }
        public int MiniDisplayPort { get; set; }
        public int DVI { get; set; }
        public int HDMI { get; set; }
        public int USBC { get; set; }
        public string PowerConnectors { get; set; }
        public string? Description { get; set; }

        /*
        *  RELACJE
        */

        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
