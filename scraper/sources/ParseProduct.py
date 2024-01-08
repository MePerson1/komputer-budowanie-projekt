class ProductNotAvailable(Exception):
    pass


class ImportantSpecNotFound(Exception):
    pass


def parse_parts(chosen_cat, specs):
    typical_problems = ["Brak danych", "Nie dotyczy", "Nie posiada"]
    
    if chosen_cat == "storage-hdd":
        translated = {
            "Name": specs["Nazwa"].replace('"', " cala"),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Description": None,
            "Type": "HDD",
            "Model": specs["Linia"],
            "FormFactor": specs["Format dysku"].replace('"', ' cala'),
            "Capacity": specs["Pojemność dysku"],
            "Interface": specs["Interfejs"].replace("III", "3").replace("II", "2").replace("I", "1"),
            "ThiccnessMM": float(specs["Grubość [mm]"]) if specs["Grubość [mm]"] not in typical_problems else None,
            "CacheMemory": specs["Pamięć podręczna"] if specs["Pamięć podręczna"] not in typical_problems else None,
            "NoiseLevelDB": float(specs["Poziom hałasu"].replace(" dB", "")) if specs["Poziom hałasu"] not in typical_problems else None,
            "RotatingSpeedRPM": int(specs["Prędkość obrotowa"].split()[0]),
            "WeightG": float(specs["Waga [g]"]) if specs["Waga [g]"] not in typical_problems else None
        }
    elif chosen_cat == "storage-ssd":
        translated = {
            "Name": specs["Nazwa"].replace('"', " cala"),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Description": None,
            "Type": "SSD",
            "Model": specs["Model"],
            "FormFactor": specs["Format dysku"].replace('"', ' cala'),
            "Capacity": specs["Pojemność dysku"],
            "Interface": specs["Interfejs"].replace("III", "3").replace("II", "2").replace("I", "1"),
            "ThiccnessMM": float(specs["Grubość"].split()[0]) if specs["Grubość"] not in typical_problems else None,
            "CacheMemory": specs["Pamięć podręczna"] if specs["Pamięć podręczna"] not in typical_problems else None,
            "Radiator": True if specs["Radiator"] == "Tak" else False,
            "MemoryChipType": specs["Rodzaj kości pamięci"] if specs["Rodzaj kości pamięci"] not in typical_problems else None,
            "ReadSpeedMBs": float(specs["Szybkość odczytu"].split()[0]),
            "WriteSpeedMBs": float(specs["Szybkość zapisu"].split()[0]) if specs["Szybkość zapisu"] not in typical_problems else None,
            "ReadRandomIOPS": int(specs["Odczyt losowy"].split()[0]) if specs["Odczyt losowy"] not in typical_problems else None,
            "WriteRandomIOPS": int(specs["Zapis losowy"].split()[0]) if specs["Zapis losowy"] not in typical_problems else None,
            "Longevity": specs["Nominalny czas pracy"] if specs["Nominalny czas pracy"] not in typical_problems else None,
            "TBW": specs["TBW (Total Bytes Written)"] if specs["TBW (Total Bytes Written)"] not in typical_problems else None,
            "Key": specs["Klucz"] if specs["Klucz"] not in typical_problems else None,
            "Controler": specs["Kontroler"] if specs["Kontroler"] not in typical_problems else None,
            "HardwareEncryption": True if specs["Szyfrowanie sprzętowe"] == "Tak" else False
        }
    elif chosen_cat == "graphic-card":
        if specs["Długość karty"] in typical_problems:
            raise ImportantSpecNotFound("GraphicCard: CardLengthMM not found")

        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "ChipsetProducer": specs["Producent chipsetu"],
            "ChipsetType": specs["Rodzaj chipsetu"],
            "CoreClockMHz": int(specs["Taktowanie rdzenia"].split()[0]) if specs["Taktowanie rdzenia"] not in typical_problems else None,
            "BoostClockMHz": int(specs["Taktowanie rdzenia w trybie boost"].split()[0]) if specs["Taktowanie rdzenia w trybie boost"] not in typical_problems else None,
            "StreamProcessors": int(specs["Procesory strumieniowe"]) if specs["Procesory strumieniowe"] not in typical_problems else None,
            "ROPUnits": int(specs["Jednostki ROP"]) if specs["Jednostki ROP"] not in typical_problems else None,
            "TextureUnits": int(specs["Jednostki teksturujące"]) if specs["Jednostki teksturujące"] not in typical_problems else None,
            "RTCores": int(specs["Rdzenie RT"]) if specs["Rdzenie RT"] != "Brak" else 0,
            "TensorCores": int(specs["Rdzenie Tensor"]) if specs["Rdzenie Tensor"] != "Brak" else 0,
            "HasDLSS3Support": True if specs.get("DLSS 3.0") == "Tak" else False,
            "ConnectorType": specs["Typ złącza"],
            "CardLengthMM": int(specs["Długość karty"].split()[0]),
            "CardLinking": specs["Łączenie kart"] if specs["Łączenie kart"] != "Nie" else None,
            "Resolution": specs["Rozdzielczość"],
            "RecommendedPSUCapacityW": int(specs["Rekomendowana moc zasilacza"].split()[0]),
            "HasLEDLighting": True if specs["Podświetlenie LED"] == "Tak" else False,
            "MemorySizeGB": int(specs["Ilość pamięci RAM"].split()[0]),
            "MemoryType": specs["Rodzaj pamięci RAM"],
            "MemoryBusWidthBits": int(specs["Szyna danych"].split()[0]),
            "MemoryClockMHz": int(specs["Taktowanie pamięci"].split()[0]),
            "CoolingType": specs["Typ chłodzenia"],
            "FanCount": int(specs["Ilość wentylatorów"]) if specs["Ilość wentylatorów"] not in typical_problems and specs["Ilość wentylatorów"] != "Brak" else 0,
            "DSub": int(specs["D-Sub"]) if specs["D-Sub"] != "Brak" else 0,
            "DisplayPortCount": int(specs["DisplayPort"]) if "DisplayPort" in specs and specs.get("DisplayPort") != "Brak" else 0,
            "MiniDisplayPort": int(specs["MiniDisplayPort"]) if specs["MiniDisplayPort"] != "Brak" else 0,
            "DVI": int(specs["DVI"]) if specs["DVI"] != "Brak" else 0,
            "HDMI": int(specs["HDMI"]) if specs["HDMI"] != "Brak" else 0,
            "USBC": int(specs["USB-C"]) if specs["USB-C"] != "Brak" else 0,
            "PowerConnectors": specs["Złącza zasilania"],
            "Description": None
        }
    elif chosen_cat == "case":
        if specs["Maksymalna długość karty graficznej [cm]"] in typical_problems:
            raise ImportantSpecNotFound("Case: MaxGPULengthCM not found")
        if specs["Maksymalna wysokość układu chłodzenia CPU [cm]"] in typical_problems:
            raise ImportantSpecNotFound("Case: MaxCoolingSystemHeightCM not found")

        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Color": specs["Kolor"],
            "HasLightning": True if specs["Podświetlenie"] == "Tak" else False,
            "HeightCM": float(specs["Wysokość [cm]"]),
            "LengthCM": float(specs["Głębokość [cm]"]),
            "WidthCM": float(specs["Szerokość [cm]"]),
            "WeightKG": float(specs["Waga [kg]"]) if specs["Waga [kg]"] not in typical_problems else None,
            "CaseType": specs["Typ obudowy"],
            "Compatibility": specs["Kompatybilność"].replace("\n", ""),
            "HasWindow": True if specs["Okno"] == "Tak" else False,
            "IsMuted": True if specs["Wyciszona"] == "Tak" else False,
            "MaxGPULengthCM": float(specs["Maksymalna długość karty graficznej [cm]"]),
            "MaxCoolingSystemHeightCM": float(specs["Maksymalna wysokość układu chłodzenia CPU [cm]"]),
            "USBTwoCount": int(specs["USB 2.0"]) if specs["USB 2.0"] != "Brak" else 0,
            "USBThreeCount": int(specs["USB 3.0"]) if specs["USB 3.0"] != "Brak" else 0,
            "USBThreePointOneCount": int(specs["USB 3.1"]) if specs["USB 3.1"] != "Brak" else 0,
            "USBThreePointTwoCount": int(specs["USB 3.2"]) if specs["USB 3.2"] != "Brak" else 0,
            "USBTypeCCount": int(specs["USB Typ-C"]) if specs["USB Typ-C"] != "Brak" else 0,
            "USBTurboChargingCount": int(specs["USB TurboCharging"]) if specs["USB TurboCharging"] != "Brak" else 0,
            "HasMemoryCardReader": True if specs["Czytnik kart pamięci"] == "Tak" else False,
            "HasAudioPort": True if specs["Złącze słuchawkowe/głośnikowe"] == "Tak" else False,
            "HasMicrophonePort": True if specs["Złącze mikrofonowe"] == "Tak" else False,
            "InternalBaysTwoPointFiveInch": int(specs["Wnęki wewnętrzne 2.5 cala"]) if specs["Wnęki wewnętrzne 2.5 cala"] != "Nie" else 0,
            "InternalBaysThreePointFiveInch": int(specs["Wnęki wewnętrzne 3.5 cala"]) if specs["Wnęki wewnętrzne 3.5 cala"] != "Nie" else 0,
            "ExternalBaysThreePointFiveInch": int(specs["Wnęki zewnętrzne 3.5 cala"]) if specs["Wnęki zewnętrzne 3.5 cala"] != "Nie" else 0,
            "ExternalBaysFivePointTwoFiveInch": int(specs["Wnęki zewnętrzne 5.25 cala"]) if specs["Wnęki zewnętrzne 5.25 cala"] != "Nie" else 0,
            "ExpansionSlots": int(specs["Sloty rozszerzeń"]),
            "PanelFront": specs["Panel przedni"] if specs["Panel przedni"] != "Nie" else None,
            "PanelRear": specs["Panel tylny"] if specs["Panel tylny"] != "Nie" else None,
            "PanelSide": specs["Panel boczny"] if specs["Panel boczny"] != "Nie" else None,
            "PanelBottom": specs["Panel dolny"] if specs["Panel dolny"] != "Nie" else None,
            "PanelTop": specs["Panel górny"] if specs["Panel górny"] != "Nie" else None,
            "PowerSupply": specs["Zasilacz"] if specs["Zasilacz"] != "Nie" else None,
            "PowerSupplyPower": specs["Moc zasilacza"].split()[0] if specs["Moc zasilacza"] != "Brak zasilacza" else None,
            "Description": None
        }
    elif chosen_cat == "ram":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Description": None,
            "PinType": specs["Typ złącza"],
            "MemoryType": specs["Typ pamięci"],
            "LowProfile": True if specs["Niskoprofilowe"] == "Tak" else False,
            "Cooling": specs["Chłodzenie"],
            "CapacityGB": int(specs["Pojemność łączna"].split()[0]),
            "ModuleCount": int(specs["Liczba modułów"]),
            "FrequencyMHz": int(specs["Częstotliwość pracy [MHz]"]),
            "LatencyCL": int(specs["Opóźnienie"].replace("CL", "")),
            "VoltageV": float(specs["Napięcie [V]"]) if specs["Napięcie [V]"] not in typical_problems else None,
            "OverclockingProfile": specs["Technologia podkręcania"].replace("\n", "") if specs["Technologia podkręcania"] not in typical_problems else None,
            "Color": specs["Kolor"],
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False
        }
    elif chosen_cat == "motherboard":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "BoardStandard": specs["Standard płyty"],
            "WidthMM": float(specs["Szerokość [mm]"]),
            "DepthMM": float(specs["Głębokość [mm]"]),
            "Chipset": specs["Chipset płyty"],
            "CPUSocket": specs["Gniazdo procesora"].split(" (")[0],
            "SupportedProcessors": specs["Obsługiwane procesory"].replace("\n", ""),
            "RAIDController": specs["Kontroler RAID"].replace("\n", "") if specs["Kontroler RAID"] != "Nie" else None,
            "MemoryStandard": specs["Standard pamięci"],
            "MemoryConnectorType": specs["Rodzaj złącza"].replace("-", ""),
            "MemorySlotsCount": int(specs["Liczba slotów pamięci"]),
            "SupportedMemoryFreq": specs["Częstotliwości pracy pamięci"].replace("\n", ""),
            "MaxMemoryGB": int(specs["Maksymalna ilość pamięci"].replace(" GB", "")),
            "ChannelArchitecture": specs["Architektura wielokanałowa"],
            "HasIntegratedGraphicsSupport": True if specs["Obsługa zintegrowanych układów graficznych"] == "Tak" else False,
            "GraphicsChipset": specs["Chipset graficzny"],
            "CardLinking": specs["Łączenie kart graficznych"] if specs["Łączenie kart graficznych"] != "Nie" else None,
            "SoundChipset": specs["Chipset dźwiękowy"].replace("\n", ""),
            "AudioChannels": specs["Kanały audio"].replace("\n", ""),
            "IntegratedNetworkCard": specs["Zintegrowana karta sieciowa"].replace("\n", ""),
            "NetworkChipset": specs["Chipset karty sieciowej"].replace("\n", ""),
            "WirelessSupport": specs["Praca bezprzewodowa"].replace("\n", "") if specs["Praca bezprzewodowa"] != "Nie" else None,
            "ExpansionSlots": specs["Gniazda rozszerzeń"].replace("\n", ""),
            "DriveConnectors": specs["Złącza napędów"].replace("\n", ""),
            "InternalConnectors": specs["Złącza wewnętrzne"].replace("\n", ""),
            "RearPanelConnectors": specs["Panel tylny"].replace("\n", ""),
            "IncludedAccessories": specs["Załączone wyposażenie"].replace("\n", "") if specs.get("Załączone wyposażenie") else None,
        }
    elif chosen_cat == "cpu":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Line": specs["Linia"],
            "HasIncludedCooling": True if specs["Załączone chłodzenie"] == "Tak" else False,
            "SocketType": specs["Typ gniazda"].split(" (")[0],
            "NumberOfCores": int(specs["Liczba rdzeni"]),
            "NumberOfThreads": int(specs["Liczba wątków"]),
            "ProcessorBaseFrequencyGHz": float(specs["Częstotliwość taktowania procesora"].replace(" GHz", "")),
            "MaxTurboFrequencyGHz": float(specs["Częstotliwość maksymalna Turbo"].replace(" GHz", "")) if specs["Częstotliwość maksymalna Turbo"] not in typical_problems else None,
            "IntegratedGraphics": specs["Zintegrowany układ graficzny"] if specs["Zintegrowany układ graficzny"] not in typical_problems else None,
            "HasUnlockedMultiplier": True if specs["Odblokowany mnożnik"] == "Tak" else False,
            "Architecture": specs["Architektura"],
            "ManufacturingProcess": specs["Proces technologiczny"],
            "ProcessorMicroarchitecture": specs["Mikroarchitektura procesora"],
            "TDPinW": int(specs["TDP"].replace(" W", "")),
            "MaxOperatingTempC": int(specs.get("Maksymalna temperatura pracy").replace(" st. C", "")) if specs.get("Maksymalna temperatura pracy") is not None else None,
            "SupportedMemoryTypes": specs["Rodzaje obsługiwanej pamięci"].replace("\n", "") if specs["Rodzaje obsługiwanej pamięci"] not in typical_problems else None,
            "L1Cache": specs["Pamięć podręczna L1"].replace("\n", ""),
            "L2Cache": specs["Pamięć podręczna L2"],
            "L3Cache": specs["Pamięć podręczna L3"],
            "AddedEquipment": specs.get("Załączone wyposażenie")
        }
    elif chosen_cat == "power-supply":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "FormFactor": specs["Standard/Format"],
            "PowerW": int(specs["Moc"].split('W')[0].strip()),
            "Certificate": specs["Certyfikat sprawności"] if specs["Certyfikat sprawności"] != "Brak" else None,
            "PowerFactorCorrection": specs["Układ PFC"],
            "EfficiencyRating": specs["Sprawność"] if specs["Sprawność"] not in typical_problems else None,
            "Cooling": specs["Typ chłodzenia"],
            "FanDiameterMM": int(specs["Średnica wentylatora"].split(' ')[0]) if specs["Średnica wentylatora"] not in typical_problems else None,
            "Security": specs["Zabezpieczenia"].replace("\n", ""),
            "ModularCabling": specs["Modularne okablowanie"] if specs["Modularne okablowanie"] != "Nie" else None,
            "ATX24Pin_20Plus4": int(specs["ATX 24-pin (20+4)"]) if specs["ATX 24-pin (20+4)"] != "Nie" else 0,
            "PCIE8Pin_6Plus2": int(specs["PCI-E 8-pin (6+2)"]) if specs["PCI-E 8-pin (6+2)"] != "Nie" else 0,
            "PCIE16Pin": int(specs["12VHPWR PCI-E 5.0 16-pin (12+4)"]) if specs["12VHPWR PCI-E 5.0 16-pin (12+4)"] != "Nie" else 0,
            "PCIE8Pin": int(specs["PCI-E 8-pin"]) if specs["PCI-E 8-pin"] not in ["Nie", "Brak danych"] else 0,
            "PCIE6Pin": int(specs["PCI-E 6-pin"]) if specs["PCI-E 6-pin"] != "Nie" else 0,
            "CPU8Pin_4Plus4": int(specs["CPU 8-pin (4+4)"]) if specs["CPU 8-pin (4+4)"] != "Nie" else 0,
            "CPU8Pin": int(specs["CPU 8-pin"]) if specs["CPU 8-pin"] != "Nie" else 0,
            "CPU4Pin": int(specs["CPU 4-pin"]) if specs["CPU 4-pin"] != "Nie" else 0,
            "Sata": int(specs["SATA"]) if specs["SATA"] != "Nie" else 0,
            "Molex": int(specs["Molex"]) if specs["Molex"] != "Nie" else 0,
            "HeightMM": float(specs["Wysokość [mm]"]) if specs.get("Wysokość [mm]") not in typical_problems else None,
            "WidthMM": float(specs["Szerokość [mm]"]) if specs.get("Szerokość [mm]") not in typical_problems else None,
            "DepthMM": float(specs["Głębokość [mm]"]) if specs.get("Głębokość [mm]") not in typical_problems else None,
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False
        }
    elif chosen_cat == "cpu-cooling":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "MountingType": specs["Sposób montażu"].replace("\n", ""),
            "ColorElement": specs["Element kolorystyczny"],
            "HeightMM": float(specs["Wysokość [mm]"]),
            "WidthMM": float(specs["Szerokość [mm]"]),
            "DepthMM": float(specs["Głębokość [mm]"]),
            "WeightGrams": float(specs["Waga [g]"]) if specs["Waga [g]"] not in typical_problems else None,
            "ProcessorSocket": specs["Socket procesora"].replace("\n", ""),
            "MaxTDPinW": int(specs["Maksymalne TDP"].split()[0]) if specs["Maksymalne TDP"] not in typical_problems else None,
            "BaseMaterial": specs["Materiał podstawy"],
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False,
            "HeatPipesCount": int(specs["Ilość ciepłowodów"]) if specs["Ilość ciepłowodów"] not in typical_problems else 0,
            "HeatPipeDiameterMM": int(specs["Średnica ciepłowodów"].split()[0]) if specs["Średnica ciepłowodów"] not in typical_problems else None,
            "FanCount": int(specs["Ilość wentylatorów"]),
            "FanDiameterMM": int(specs["Średnica wentylatora"].split()[0]) if specs["Średnica wentylatora"] not in typical_problems else None,
            "MaxFanSpeedPerMin": int(specs["Maksymalna prędkość obrotowa"].split()[0]) if specs["Maksymalna prędkość obrotowa"] not in typical_problems else None,
            "MaxNoiseLevelinDBA": float(specs["Maksymalny poziom hałasu"].split()[0]) if specs["Maksymalny poziom hałasu"] not in typical_problems else None,
            "AirflowCFM": float(specs["Przepływ powietrza [CFM]"]) if specs["Przepływ powietrza [CFM]"] not in typical_problems else None,
            "LifespanHours": int(specs["Żywotność"].split()[0]) if specs["Żywotność"] not in typical_problems else None,
        }
    elif chosen_cat == "water-cooling":
        translated = {
            "Name": specs["Nazwa"],
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "IntelCompatibility": specs["Kompatybilność z procesorami Intel"].replace("\n", ""),
            "AMDCompatibility": specs["Kompatybilność z procesorami AMD"].replace("\n", ""),
            "Lighting": specs["Podświetlenie"] if specs["Podświetlenie"] != "Brak" else None,
            "WeightG": int(specs["Waga [g]"]) if specs["Waga [g]"] not in typical_problems else None,
            "RadiatorSizeMM": float(specs["Rozmiar chłodnicy"].split()[0]),
            "RadiatorLengthMM": float(specs["Długość chłodnicy [mm]"]),
            "RadiatorWidthMM": float(specs["Szerokość chłodnicy [mm]"]),
            "RadiatorHeightMM": float(specs["Wysokość chłodnicy [mm]"]),
            "FanCount": int(specs["Liczba wentylatorów"]),
            "FanDiameterMM": int(specs["Średnica wentylatora"].split()[0]),
            "MaxFanSpeedRPM": int(specs["Maksymalna prędkość obrotowa"].split()[0]),
            "HasPWMControl": True if specs["Regulacja obrotów PWM"] == "Tak" else False,
            "MaxAirflowCFM": float(specs["Maksymalny przepływ powietrza"].split()[0]) if specs["Maksymalny przepływ powietrza"] not in typical_problems else None,
            "MaxNoiseLevelDBa": float(specs["Maksymalny poziom hałasu"].split()[0]) if specs["Maksymalny poziom hałasu"] not in typical_problems else None,
            "FanConnector": specs["Złącze wentylatora"],
            "PumpConnector": specs["Złącze pompy"] if specs["Złącze pompy"] not in typical_problems else None,
            "LEDConnector": specs["Złącze podświetlenia LED"] if specs["Złącze podświetlenia LED"] not in typical_problems else None
        }
    else:
        translated = specs
    return translated
