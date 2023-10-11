namespace KomputerBudowanieAPI.Dto
{
    public class CpuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        public int CoreCount { get; set; }
        public int ThreadsCount { get; set; }

        public string SocketType { get; set; }
        public string MemoryType { get; set; }

        public string CacheSize { get; set; }

        public double ClockFrequency { get; set; }
        public double TurboClockFrequency { get; set; }

        public int TDP { get; set; }
        public int MTP { get; set; }

        public bool IntegratedGraphics { get; set; }
        public bool HyperThreading { get; set; }
        public bool OverclockingSupport { get; set; }
    }
}
