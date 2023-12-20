using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Case
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice> Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string Color { get; set; }
        public bool HasLightning { get; set; }
        public float HeightCM { get; set; }
        public float LengthCM { get; set; }
        public float WidthCM { get; set; }
        public float WeightKG { get; set; }
        public string CaseType { get; set; }
        public string Compatibility { get; set; }
        public bool HasWindow { get; set; }
        public bool IsMuted { get; set; }
        public float MaxGPULengthCM { get; set; }
        public float MaxCoolingSystemHeightCM { get; set; }

        public int USBTwoCount { get; set; }
        public int USBThreeCount { get; set; }
        public int USBThreePointOneCount { get; set; }
        public int USBThreePointTwoCount { get; set; }
        public int USBTypeCCount { get; set; }
        public int USBTurboChargingCount { get; set; }

        public bool HasMemoryCardReader { get; set; }
        public bool HasAudioPort { get; set; }
        public bool HasMicrophonePort { get; set; }

        public int InternalBaysTwoPointFiveInch { get; set; }
        public int InternalBaysThreePointFiveInch { get; set; }
        public int ExternalBaysThreePointFiveInch { get; set; }
        public int ExternalBaysFivePointTwoFiveInch { get; set; }

        public int ExpansionSlots { get; set; }

        public string? PanelFront { get; set; }
        public string? PanelRear { get; set; }
        public string? PanelSide { get; set; }
        public string? PanelBottom { get; set; }
        public string? PanelTop { get; set; }

        public string? PowerSupply { get; set; }
        public float? PowerSupplyPower { get; set; }
        public string? Description { get; set; }

        /*
         *  RELACJE
         */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
