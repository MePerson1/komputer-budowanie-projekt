using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class Ram
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        public float Height { get; set; }
        public float Voltage { get; set; }
        public int LatencyCL { get; set; }
        public int Speed { get; set; }
        public bool ECC { get; set; } //Error Checking and Correction

        /*
        *  RELACJE
        */

        public ICollection<PcConfiguration> PcConfigurations { get; set; }
    }
}
