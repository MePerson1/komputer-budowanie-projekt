using KomputerBudowanieAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationDto
    {
        public Guid Id { get; set; }
        //[Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? MotherboadId { get; set; }
        public string? GraphicCardId { get; set; }
        public string? CpuId { get; set; }
        public string? CpuCoolingId { get; set; }
        public string? CaseId { get; set; }
        public string? PowerSuplyId { get; set; }

        //[Required]
        public int UserId { get; set; }
    }
}
