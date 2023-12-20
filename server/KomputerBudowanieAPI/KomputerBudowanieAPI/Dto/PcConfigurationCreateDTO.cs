namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationCreateDto
    {
        //[Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? MotherboadId { get; set; }
        public int? GraphicCardId { get; set; }
        public int? CpuId { get; set; }
        public int? CpuCoolingId { get; set; }
        public int? CaseId { get; set; }
        public int? PowerSuplyId { get; set; }

        public int UserId { get; set; }

        public ICollection<int>? MemoryIds { get; set; }
        public ICollection<int>? RamsIds { get; set; }

    }
}
