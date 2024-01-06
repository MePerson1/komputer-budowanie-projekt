using KomputerBudowanieAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class CpuCooling : IProduct
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string MountingType { get; set; }
        public string ColorElement { get; set; }
        public float HeightMM { get; set; }
        public float WidthMM { get; set; }
        public float DepthMM { get; set; }
        public float? WeightGrams { get; set; }
        public string ProcessorSocket { get; set; }
        public int? MaxTDPinW { get; set; }
        public string BaseMaterial { get; set; }
        public bool HasLighting { get; set; }
        public int HeatPipesCount { get; set; }
        public int? HeatPipeDiameterMM { get; set; }
        public int FanCount { get; set; }
        public int? FanDiameterMM { get; set; }
        public int? MaxFanSpeedPerMin { get; set; }
        public float? MaxNoiseLevelinDBA { get; set; }
        public float? AirflowCFM { get; set; }
        public int? LifespanHours { get; set; }

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
