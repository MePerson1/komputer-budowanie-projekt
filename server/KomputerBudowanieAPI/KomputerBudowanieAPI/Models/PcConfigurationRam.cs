using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationRam
    {
        [JsonIgnore]
        public Guid PcConfigurationsId { get; set; }
        [JsonIgnore]
        public int RamsId { get; set; }
        [JsonIgnore]
        public PcConfiguration PcConfiguration { get; set; } = null!;
        public Ram Ram { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
