using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public string Series { get; set; }
        public string Model { get; set; }
        public string MemoryType { get; set; }
        public int MemorySize { get; set; }
        public int MemoryBus { get; set; }
        public int MemoryBandwith { get; set; }
        public int MemoryClockspeed { get; set; }
        public int CoreClockspeed { get; set; }
        public bool RayTraycing { get; set; }
        public int FanCount { get; set; }
        public int PsuPower { get; set; }
        public string CoolingType { get; set; }


        /*
         * DO EDYCJI!
         */
        //private Dictionary<string, int> OuterConnectors { get; set; }
        //private Dictionary<string, int> InnerConnectors { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public int Slots { get; set; }

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
