using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class PcConfiguration
    {

        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public Motherboard? Motherboard { get; set; }
        public GraphicCard? GraphicCard { get; set; }
        public Cpu? Cpu { get; set; }
        public CpuCooling? CpuCooling { get; set; } = null;

        public Case? Case { get; set; }
        public PowerSupply? PowerSupply { get; set; } = null;
        public WaterCooling? WaterCooling { get; set; }
        public User? User { get; set; } = null;
        //public double TotalPrice { get; set; } = 0;
        public ICollection<PcConfigurationStorage>? PcConfigurationStorages { get; set; }
        public ICollection<PcConfigurationRam>? PcConfigurationRams { get; set; }
    }
}
