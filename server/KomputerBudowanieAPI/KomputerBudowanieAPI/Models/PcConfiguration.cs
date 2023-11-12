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

        public string Name { get; set; }
        public string? Description { get; set; }

        public Motherboard Motherboard { get; set; }
        public GraphicCard GraphicCard { get; set; }
        public Cpu Cpu { get; set; }
        public CpuCooling CPU_Cooling { get; set; }

        public Case Case { get; set; }
        public WaterCooling Fan { get; set; } // tutaj tez many to many raczej
        public PowerSupply PowerSupply { get; set; }

        public User? User { get; set; } = null;

        public ICollection<Storage> Memories { get; set; }
        public ICollection<Ram> Rams { get; set; }
    }
}
