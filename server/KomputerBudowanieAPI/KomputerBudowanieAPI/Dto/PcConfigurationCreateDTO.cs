namespace KomputerBudowanieAPI.Dto
{
    public class PcConfigurationCreateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MotherboadId { get; set; }
        public int GraphicCardId { get; set; }
        public int CpuId { get; set; }
        public int CpuCoolingId { get; set; }
        public int CaseId { get; set; }
        public int PowerSuplyId { get; set; }

        //[Required]
        public int UserId { get; set; }
    }
}
