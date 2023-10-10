using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Models
{
    //public enum Modulairty
    //{
    //    Non,
    //    Semi,
    //    Full
    //}

    //public enum PowerFactorCorrection
    //{
    //    None,
    //    Active,
    //    Pasive
    //}

    public class PowerSupply
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }

        // Details
        public int PowerOutput { get; set; }

        /*
         * Dodanie Enum
         */
        public string Modularity { get; set; }
        public string PowerFactorCorrection { get; set; }
        //public Modulairty Modularity { get; set; }
        //public PowerFactorCorrection PowerFactorCorrection { get; set; }

        public string Protection { get; set; }


        /*
         * DO EDYCJI!
         */
        //public List<string> Rails { get; set; } // what is it xd

        /*
         * Pins / Connectors
         */
        public int ATX24_Pin { get; set; }
        public int Sata { get; set; }
        public int Molex { get; set; }

        public string EfficiencyRating { get; set; }

        public string FormFactor { get; set; }
        /*
         * Możliwe ze juz FormFactor z góry określa rozmiar
         */
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }

        /*
        *  RELACJE
        */

        public ICollection<PcConfiguration> Configurations { get; set; }
    }
}
