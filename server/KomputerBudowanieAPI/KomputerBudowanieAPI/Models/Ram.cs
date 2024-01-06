using KomputerBudowanieAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Ram : IPart
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string? Description { get; set; }
        public string PinType { get; set; }
        public string MemoryType { get; set; }
        public bool LowProfile { get; set; }
        public string Cooling { get; set; }
        public int CapacityGB { get; set; }
        public int ModuleCount { get; set; }
        public int FrequencyMHz { get; set; }
        public int LatencyCL { get; set; }
        public float? VoltageV { get; set; }
        public string? OverclockingProfile { get; set; }
        public string Color { get; set; }
        public bool HasLighting { get; set; }


        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfigurationRam>? PcConfigurationRams { get; set; }
    }
}
