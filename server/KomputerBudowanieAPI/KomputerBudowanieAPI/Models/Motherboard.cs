namespace KomputerBudowanieAPI.Models
{
    public class Motherboard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public string FormFactor { get; set; }

        public int MaxRam { get; set; }
        public string[] RamType { get; set; }
        public int RamSlotsCount { get; set; }

        public int PCIe_x16Slots { get; set; }
        public int PCIe_x4Slots { get; set; }
        public int PCIe_x1Slots { get; set; }


        /*
         * DO EDYCJI!
         */
        //public Dictionary<string, int> USBPorts { get; set; }

        //public Dictionary<string, int> VideoPorts { get; set; }

        public int SATAPorts { get; set; }
        public int M2Slots { get; set; }
        public int EthernetPorts { get; set; }
        public int AudioChannels { get; set; }

        public bool BluetoothSupport { get; set; }  // Bluetooth support (true/false)
        public bool WiFiSupport { get; set; }


        /*
         * DO EDYCJI!
         */
        //public Dictionary<string, int> AudioPorts { get; set; }
        //public Dictionary<string, int> RGBPorts { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }



    }
}
