using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class CpuCooling
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<ShopPrice> Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }

        public string MountingType { get; set; } // Sposób montażu
        public string ColorElement { get; set; } // Element kolorystyczny
        public float HeightMM { get; set; }
        public float WidthMM { get; set; }
        public float DepthMM { get; set; }
        public int WeightGrams { get; set; }
        public string ProcessorSocket { get; set; } // Socket procesora
        public int? MaxTDPinW { get; set; } // Maksymalne TDP
        public string BaseMaterial { get; set; } // Materiał podstawy
        public bool HasLighting { get; set; } // Podświetlenie
        public int HeatPipesCount { get; set; } // Ilość ciepłowodów
        public int HeatPipeDiameterMM { get; set; } // Średnica ciepłowodów
        public int FanCount { get; set; } // Ilość wentylatorów
        public int FanDiameterMM { get; set; }
        public int? MaxFanSpeedPerMin { get; set; } // Maksymalna prędkość obrotowa
        public float? AirflowCFM { get; set; } // Przepływ powietrza [CFM]
        public float? MaxNoiseLevelinDBA { get; set; } // Maksymalny poziom hałasu
        public int? LifespanHours { get; set; } // Żywotność

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
