using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class CpuCooling
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        public string CoolingType { get; set; }
        public string[] Sockets { get; set; }
        public int TDP { get; set; }
        public int RPM { get; set; }
        public string Connectr { get; set; }
        public double NoiseLevel { get; set; }
        public int FanCount { get; set; }

        public int TowerCount { get; set; }
        public int Voltage { get; set; }
        public string Material { get; set; }

        public bool LiuquidCooling { get; set; }
        public bool RGBSupport { get; set; }

        public double Height { get; set; }
        public double Width { get; set; }

        /*
        *  RELACJE
        */

        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
