using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class WaterCooling
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string IntelCompatibility { get; set; }
        public string AMDCompatibility { get; set; }
        public string? Lighting { get; set; }
        public int? WeightG { get; set; }
        public float RadiatorSizeMM { get; set; }
        public float RadiatorLengthMM { get; set; }
        public float RadiatorWidthMM { get; set; }
        public float RadiatorHeightMM { get; set; }
        public int FanCount { get; set; }
        public int FanDiameterMM { get; set; }
        public int MaxFanSpeedRPM { get; set; }
        public bool HasPWMControl { get; set; }
        public float? MaxAirflowCFM { get; set; }
        public float? MaxNoiseLevelDBa { get; set; }
        public string FanConnector { get; set; }
        public string? PumpConnector { get; set; }
        public string? LEDConnector { get; set; }

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
