using KomputerBudowanieAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string MotherboadId { get; set; }
        public string GraphicCardId { get; set; }
        public string CpuId { get; set; }
        public string CpuCoolingId { get; set; }
        public string CaseId { get; set; }
        public string PowerSuplyId { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<PcConfigurationMemory> PcConfigurationMemories { get; set; }
        public ICollection<PcConfigurationRam> PcConfigurationRams { get; set; }

    }
}
