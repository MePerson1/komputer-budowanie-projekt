using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class GraphicCard
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        private string Series { get; set; }
        private string Model { get; set; }
        private string MemoryType { get; set; }
        private int MemorySize { get; set; }
        private int MemoryBus { get; set; }
        private int MemoryBandwith { get; set; }
        private int MemoryClockspeed { get; set; }
        private int CoreClockspeed { get; set; }
        private bool RayTraycing { get; set; }
        private int FanCount { get; set; }
        private int PsuPower { get; set; }
        private string CoolingType { get; set; }


        /*
         * DO EDYCJI!
         */
        //private Dictionary<string, int> OuterConnectors { get; set; }
        //private Dictionary<string, int> InnerConnectors { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private float Length { get; set; }
        private int Slots { get; set; }
    }
}
