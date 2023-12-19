using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class WaterCooling
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public ICollection<ShopPrice> Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }

        public string IntelCompatibility { get; set; } // Kompatybilność z procesorami Intel
        public string AMDCompatibility { get; set; } // Kompatybilność z procesorami AMD
        public string? Lighting { get; set; } // Podświetlenie
        public int? WeightG { get; set; }
        public float RadiatorSizeMM { get; set; } // Rozmiar chłodnicy
        public float RadiatorLengthMM { get; set; } // Długość chłodnicy [mm]
        public float RadiatorWidthMM { get; set; } // Szerokość chłodnicy [mm]
        public float RadiatorHeightMM { get; set; } // Wysokość chłodnicy [mm]
        public int FanCount { get; set; } // Liczba wentylatorów
        public int FanDiameterMM { get; set; } // Średnica wentylatora
        public int MaxFanSpeedRPM { get; set; } // Maksymalna prędkość obrotowa
        public bool HasPWMControl { get; set; } // Regulacja obrotów PWM
        public float? MaxAirflowCFM { get; set; } // Maksymalny przepływ powietrza
        public float? MaxNoiseLevelDBa { get; set; } // Maksymalny poziom hałasu
        public string FanConnector { get; set; } // Złącze wentylatora
        public string? PumpConnector { get; set; } // Złącze pompy
        public string? LEDConnector { get; set; } // Złącze podświetlenia LED

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
