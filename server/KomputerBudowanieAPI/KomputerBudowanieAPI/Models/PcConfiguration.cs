using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class PcConfiguration
    {
        /*
         * klucze obce danych komponentów (nie wszystkie muszą być dodane i guess)
         * 
         */
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; } = "Moja konfiguracja";
        public string? Description { get; set; }

        public Motherboard? Motherboard { get; set; }
        public GraphicCard? GraphicCard { get; set; }
        public Cpu? Cpu { get; set; }
        public CpuCooling? CPU_Cooling { get; set; } = null;

        public Case? Case { get; set; }
        public PowerSupply? PowerSupply { get; set; } = null;
        public WaterCooling? WaterCooling { get; set; }
        public int? UserId { get; set; } // Nullable foreign key to User
        public User? User { get; set; }

        public ICollection<Fan>? Fans { get; set; }
        public ICollection<Storage>? Storages { get; set; }
        public ICollection<Ram>? Rams { get; set; }

        [JsonIgnore]
        public ICollection<User>? UsersFavorited { get; set; }
    }
}
