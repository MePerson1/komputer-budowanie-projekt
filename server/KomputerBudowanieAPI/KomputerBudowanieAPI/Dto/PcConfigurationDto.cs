namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationDto
    {
        public Guid Id { get; set; }
        //[Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? MotherboadId { get; set; }
        public int? GraphicCardId { get; set; }
        public int? CpuId { get; set; }
        public int? CpuCoolingId { get; set; }

        public int? WaterCoolingId { get; set; }
        public int? CaseId { get; set; }
        public int? PowerSuplyId { get; set; }

        //[Required]
        public int UserId { get; set; }

        public ICollection<int>? StorageIds { get; set; }
        public ICollection<int>? RamsIds { get; set; }
        public ICollection<int>? FanIds { get; set; }

    }
}