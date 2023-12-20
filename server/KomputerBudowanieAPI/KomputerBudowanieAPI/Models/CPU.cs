using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Cpu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string Line { get; set; }
        public bool HasIncludedCooling { get; set; }
        public string SocketType { get; set; }
        public int NumberOfCores { get; set; }
        public int NumberOfThreads { get; set; }
        public float ProcessorBaseFrequencyGHz { get; set; }
        public float? MaxTurboFrequencyGHz { get; set; }
        public string? IntegratedGraphics { get; set; }
        public bool HasUnlockedMultiplier { get; set; }
        public string Architecture { get; set; }
        public string ManufacturingProcess { get; set; }
        public string ProcessorMicroarchitecture { get; set; }
        public int TDPinW { get; set; }
        public int? MaxOperatingTempC { get; set; }
        public string? SupportedMemoryTypes { get; set; }
        public string L1Cache { get; set; }
        public string L2Cache { get; set; }
        public string L3Cache { get; set; }
        public string? AddedEquipment { get; set; }

        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}