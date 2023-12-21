using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Dto
{
    public class MotherboardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopPrice>? Prices { get; set; }
        public string Producer { get; set; }
        public string ProducerCode { get; set; }

        // Board Details
        public string BoardStandard { get; set; } // Standard płyty
        public float WidthMM { get; set; } // Szerokość [mm]
        public float DepthMM { get; set; } // Głębokość [mm]
        public string Chipset { get; set; } // Chipset płyty
        public string CPUSocket { get; set; } // Gniazdo procesora
        public string SupportedProcessors { get; set; } // Obsługiwane procesory (Intel Celeron, Intel Core i3, etc.)
        public string? RAIDController { get; set; } // Kontroler RAID
        public string MemoryStandard { get; set; } // Standard pamięci
        public string MemoryConnectorType { get; set; } // Rodzaj złącza
        public int MemorySlotsCount { get; set; } // Liczba slotów pamięci
        public string SupportedMemoryFreq { get; set; } // Częstotliwości pracy pamięci
        public int MaxMemoryGB { get; set; } // Maksymalna ilość pamięci
        public string ChannelArchitecture { get; set; } // Architektura wielokanałowa
        public bool HasIntegratedGraphicsSupport { get; set; } // Obsługa zintegrowanych układów graficznych
        public string GraphicsChipset { get; set; } // Chipset graficzny
        public string? CardLinking { get; set; } // Łączenie kart graficznych
        public string SoundChipset { get; set; } // Chipset dźwiękowy
        public string AudioChannels { get; set; } // Kanały audio
        public string IntegratedNetworkCard { get; set; } // Zintegrowana karta sieciowa
        public string NetworkChipset { get; set; } // Chipset karty sieciowej
        public string? WirelessSupport { get; set; } // Praca bezprzewodowa
        public string ExpansionSlots { get; set; } // Gniazda rozszerzeń
        public string DriveConnectors { get; set; } // Złącza napędów
        public string InternalConnectors { get; set; } // Złącza wewnętrzne
        public string RearPanelConnectors { get; set; } // Panel tylny
        public string? IncludedAccessories { get; set; } // Załączone wyposażenie
    }
}
