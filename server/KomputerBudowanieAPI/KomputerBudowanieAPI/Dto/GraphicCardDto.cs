namespace KomputerBudowanieAPI.Dto
{
    public class GraphicCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }
        public string ManufacturerCode { get; set; }
        public string ChipsetManufacturer { get; set; }
        public string ChipsetType { get; set; }
        public int CoreClockMHz { get; set; }
        public int BoostClockMHz { get; set; }
        public int StreamProcessors { get; set; }
        public int ROPUnits { get; set; }
        public int TextureUnits { get; set; }
        public int RTUnits { get; set; }
        public int TensorCores { get; set; }
        public bool DLSS3Supported { get; set; }
        public string ConnectorType { get; set; }
        public int CardLengthMM { get; set; }
        public string Resolution { get; set; }
        public int RecommendedPSUCapacityW { get; set; }
        public bool LEDLighting { get; set; }
        public int MemorySizeGB { get; set; }
        public string MemoryType { get; set; }
        public int MemoryBusWidthBits { get; set; }
        public int MemoryClockMHz { get; set; }
        public string CoolingType { get; set; }
        public int FanCount { get; set; }
        public int DisplayPortCount { get; set; }
        public int HDMI { get; set; }
        public string PowerConnectors { get; set; }
    }
}
