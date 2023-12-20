using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Dto
{
    public class PowerSupplyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice> Prices { get; set; }
        public string Producer { get; set; }
        public string? Description { get; set; }

        public string ProducerCode { get; set; }

        // Details
        public string FormFactor { get; set; } //standard
        public int PowerW { get; set; }
        public string Certificate { get; set; }
        public string PowerFactorCorrection { get; set; } //pfc
        public string EfficiencyRating { get; set; }
        public string Cooling { get; set; }
        public int FanDiameterMM { get; set; }
        public string Security { get; set; } //to chyba juz lepiej jako string po przecinku trzymac so far
        public string? ModularCabling { get; set; }
        /*
         * Pins / Connectors
         */
        public int ATX24Pin_20Plus4 { get; set; }
        public int PCIE8Pin_6Plus2 { get; set; }
        public int PCIE16Pin { get; set; }
        public int PCIE8Pin { get; set; }
        public int PCIE6Pin { get; set; }
        public int CPU8Pin_4Plus4 { get; set; }
        public int CPU8Pin { get; set; }
        public int CPU4Pin { get; set; }
        public int Sata { get; set; }
        public int Molex { get; set; }


        public int HeightMM { get; set; }
        public int WidthMM { get; set; }
        public int DepthMM { get; set; }
        public bool HasLighting { get; set; }
    }
}
