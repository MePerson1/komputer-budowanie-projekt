using KomputerBudowanieAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Motherboard : IPart
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string BoardStandard { get; set; }
        public float WidthMM { get; set; }
        public float DepthMM { get; set; }
        public string Chipset { get; set; }
        public string CPUSocket { get; set; }
        public string SupportedProcessors { get; set; }
        public string? RAIDController { get; set; }
        public string MemoryStandard { get; set; }
        public string MemoryConnectorType { get; set; }
        public int MemorySlotsCount { get; set; }
        public string SupportedMemoryFreq { get; set; }
        public int MaxMemoryGB { get; set; }
        public string ChannelArchitecture { get; set; }
        public bool HasIntegratedGraphicsSupport { get; set; }
        public string? GraphicsChipset { get; set; }
        public string? CardLinking { get; set; }
        public string SoundChipset { get; set; }
        public string AudioChannels { get; set; }
        public string IntegratedNetworkCard { get; set; }
        public string NetworkChipset { get; set; }
        public string? WirelessSupport { get; set; }
        public string ExpansionSlots { get; set; }
        public string DriveConnectors { get; set; }
        public string InternalConnectors { get; set; }
        public string RearPanelConnectors { get; set; }
        public string? IncludedAccessories { get; set; }

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }

    }
}
