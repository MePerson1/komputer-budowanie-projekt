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
        private float Height { get; set; }
        private float Voltage { get; set; }
        private int LatencyCL { get; set; }
        private int Speed { get; set; }
        private bool ECC { get; set; } //Error Checking and Correction
    }
}
