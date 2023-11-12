using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public MotherboardDto Motherboard { get; set; }
        public GraphicCardDto GraphicCard { get; set; }
        public CpuDto Cpu { get; set; }
        public CpuCoolingDto CPU_Cooling { get; set; }

        public CaseDto Case { get; set; }
        public WaterCoolingDto Fan { get; set; } // tutaj tez many to many raczej
        public PowerSupplyDto PowerSupply { get; set; }

        public User? User { get; set; } = null;

        public ICollection<StorageDto> Memories { get; set; }
        public ICollection<RamDto> Rams { get; set; }

    }
}
