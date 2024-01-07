using KomputerBudowanieAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class PowerSupply : IPart
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string FormFactor { get; set; }
        public int PowerW { get; set; }
        public string? Certificate { get; set; }
        public string PowerFactorCorrection { get; set; }
        public string? EfficiencyRating { get; set; }
        public string Cooling { get; set; }
        public int? FanDiameterMM { get; set; }
        public string? Security { get; set; }
        public string? ModularCabling { get; set; }

        public int ATX24Pin_20Plus4 { get; set; }
        public int PCIE8Pin_6Plus2 { get; set; }
        public int PCIE16Pin { get; set; }
        public int PCIE8Pin { get; set; }
        public int PCIE6Pin { get; set; }
        public int CPU8Pin_4Plus4 { get; set; }
        public int CPU8Pin { get; set; }
        public int CPU4Pin { get; set; }

        public int Sata { get; set; }
        public int Molex { get; set; }
        public float? HeightMM { get; set; }
        public float? WidthMM { get; set; }
        public float? DepthMM { get; set; }
        public bool HasLighting { get; set; }

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
