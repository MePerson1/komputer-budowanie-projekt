using System.ComponentModel.DataAnnotations;

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
        public CpuCooling? CpuCooling { get; set; } = null;

        public Case? Case { get; set; }
        public PowerSupply? PowerSupply { get; set; } = null;
        public WaterCooling? WaterCooling { get; set; }
        public User? User { get; set; } = null;

        public ICollection<Fan>? Fans { get; set; }
        public ICollection<Storage>? Storages { get; set; }
        public ICollection<Ram>? Rams { get; set; }
    }
}
