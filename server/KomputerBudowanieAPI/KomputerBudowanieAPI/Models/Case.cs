using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    public class Case
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public bool Lightning { get; set; }

        public string FormFactor { get; set; }

        public string MotherboardType { get; set; }
        public string PowerSupplyType { get; set; }
        public int MaxGPUSize { get; set; }


        public bool PowerSupply { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }
        public float Lenght { get; set; }

        /*
         * DO EDYCJI!
         */
        //public Dictionary<string, int> FrontPanelInputs { get; set; }
        public int External5_25Bays { get; set; }
        public int Internal3_5Bays { get; set; }
        public int Internal2_5Bays { get; set; }
        public int ExpansionSlots { get; set; }
        public bool WaterCoolingSupport { get; set; }

        /*
         *  RELACJE
         */

        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
