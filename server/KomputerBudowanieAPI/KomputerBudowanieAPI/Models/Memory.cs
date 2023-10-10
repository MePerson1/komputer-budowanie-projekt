using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class Memory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        private string Type { get; set; }
        private int Capacity { get; set; }
        private int ReadSpeed { get; set; }
        private int WriteSpeed { get; set; }
        private string Format { get; set; }
        private string Interface { get; set; }
        private float Height { get; set; }

        /*
        *  RELACJE
        */

        public ICollection<PcConfigurationMemory> PcConfigurationMemories { get; set; }
    }
}
