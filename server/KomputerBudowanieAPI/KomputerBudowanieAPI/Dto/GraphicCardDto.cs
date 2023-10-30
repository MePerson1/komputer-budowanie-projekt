namespace KomputerBudowanieAPI.Dto
{
    public class GraphicCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }
        public string ChipsetProducer { get; set; }
        public string ChipsetType { get; set; }
        public int CoreClockMHz { get; set; }
        public int BoostClockMHz { get; set; }
        public int StreamProcessors { get; set; }
        public int ROPUnits { get; set; }
        public int TextureUnits { get; set; }
        public int RTCores { get; set; }
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
        public bool HasDSub { get; set; }
        public int DisplayPortCount { get; set; }
        public bool HasMiniDisplayPort { get; set; }
        public bool HasDVI { get; set; }
        public int HDMICount { get; set; }
        public bool HasUSBC { get; set; }
        public string PowerConnectors { get; set; }
        public string? Description { get; set; }
    }
}
