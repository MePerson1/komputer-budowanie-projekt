using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class PcConfigurationStorage
    {
        [JsonIgnore]
        public Guid PcConfigurationsId { get; set; }
        [JsonIgnore]
        public int StoragesId { get; set; }
        [JsonIgnore]
        public PcConfiguration PcConfiguration { get; set; } = null!;
        public Storage Storage { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
