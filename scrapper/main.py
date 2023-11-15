import requests
from bs4 import BeautifulSoup
from requests import get
from requests import exceptions
import configparser

link_base = "https://www.morele.net"

config = configparser.ConfigParser()
config.read('config.ini')
main_route = int(config.get('Settings', 'main_route'))
parts_route = int(config.get('Settings', 'parts_route'))
cooling_route = int(config.get('Settings', 'cooling_route'))
show_raw_data_in_console = True if config.get('Settings', 'show_raw_data_in_console') == "1" else False
show_translated_data_in_console = True if config.get('Settings', 'show_translated_data_in_console') == "1" else False
add_to_database = True if config.get('Settings', 'add_to_database') == "1" else False
fill_database = True if config.get('Settings', 'fill_database') == "1" else False


def go_through_route(first_route, second_route):
    # zloz main routes
    main_routes = []
    html_main = get("https://www.morele.net/podzespoly-komputerowe/").text
    soup = BeautifulSoup(html_main, "lxml")
    main_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in main_links:
        main_routes.append(link_base + link['href'])

    # przejdz przez main route, zloz routy dla czesci
    html_parts = get(main_routes[first_route]).text
    soup = BeautifulSoup(html_parts, "lxml")
    parts_routes = []

    parts_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in parts_links:
        parts_routes.append(link_base + link['href'])

    if first_route == 2:
        # usuniecie z listy po kolei linkow do dyskow hdd z demontazu, dyskow ssd z demontazu, pamieci ram z demontazu
        # oraz tunerow TV, FM, kart wideo
        parts_routes = [link for index, link in enumerate(parts_routes) if index not in [1, 3, 11, 15]]

        # przejdz przez parts route
        html_processors = get(parts_routes[second_route]).text
    elif first_route == 0:
        # usuniecie z listy po kolei linkow do akcesorii chlodzenia wodnego, past termoprzewodzacych i termopadow
        parts_routes = [link for index, link in enumerate(parts_routes) if index not in [1, 3, 4]]
        # przejdz przez cooling route
        html_processors = get(parts_routes[second_route]).text
    else:
        html_processors = ""

    # wejdz na strone z konkretnymi produktami, zbierz do nich linki
    soup = BeautifulSoup(html_processors, "lxml")
    product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
    prod_links = []

    # utworz na tej podstawie linki do kazdego produktu na pierwszej stronie
    for link in product_links_raw:
        prod_links.append(link_base + link['href'])
    return prod_links


def get_product_specs(first_route, second_route, prod_links):
    i = 1
    all_products = []
    for link in prod_links:
        try:
            # wejdz do poszczegolnego produktu
            html_product = get(link).text
            soup = BeautifulSoup(html_product, "lxml")

            # sprawdz czy jest dostepny zanim w ogole zacznie sie jakas obrobka
            availability_raw = soup.find('div', class_='prod-available-items')
            if availability_raw is not None:
                # zgarnij wszystkie speci
                name = soup.find('h1', class_='prod-name').text
                availability = availability_raw.text.strip()
                availability = availability.replace("\n", " ")
                price = soup.find('div', class_='product-price').text.strip()

                product_specs = {"Nazwa": name,
                                 "Dostepnosc": availability,
                                 "Cena": price}
                specs_raw = soup.find_all('div', class_='specification__row')

                for row in specs_raw:
                    spec_name = row.find('span', class_='specification__name').text.strip()
                    spec_value = row.find('span', class_='specification__value').text.strip()
                    product_specs[spec_name] = spec_value

                # wyswietl rzeczy przed tlumaczeniem
                if show_raw_data_in_console:
                    print(f"\nProdukt {i}: {link}")
                    for key, value in product_specs.items():
                        print(key, ':', value)

                if first_route == 2:
                    translated_product_specs = translate_and_parse_parts(second_route, product_specs)
                elif first_route == 0:
                    translated_product_specs = translate_and_parse_cooling(second_route, product_specs)
                else:
                    translated_product_specs = {}

                if show_translated_data_in_console:
                    # wyswietl rzeczy po tlumaczeniu
                    print(f"\n(PRZETLUMACZONE) Produkt {i}: {link}")
                    for key, value in translated_product_specs.items():
                        print(key, ':', value)

                # dodaj speki produktu do listy ze wszystkimi
                if translated_product_specs != {}:
                    all_products.append(translated_product_specs)
                i += 1
        except exceptions.RequestException as req_error:
            print(f"\n\nRequest error for link {link}: {req_error}\n\n")
        except Exception as e:
            print(f"\n\nAn unexpected error occurred for link {link}: {e}\n\n")
    return all_products


def translate_and_parse_cooling(second_route, specs):
    if second_route == 0:  # chlodzenie CPU
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "MountingType": specs["Sposób montażu"].replace("\n", ""),
            "ColorElement": specs["Element kolorystyczny"],
            "HeightMM": float(specs["Wysokość [mm]"]),
            "WidthMM": float(specs["Szerokość [mm]"]),
            "DepthMM": float(specs["Głębokość [mm]"]),
            "WeightGrams": int(specs["Waga [g]"]),
            "ProcessorSocket": specs["Socket procesora"].replace("\n", ""),
            "MaxTDPinW": int(specs["Maksymalne TDP"].split()[0]) if specs["Maksymalne TDP"] != "Brak danych" else None,
            "BaseMaterial": specs["Materiał podstawy"],
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False,
            "HeatPipesCount": int(specs["Ilość ciepłowodów"]),
            "HeatPipeDiameterMM": int(specs["Średnica ciepłowodów"].split()[0]),
            "FanCount": int(specs["Ilość wentylatorów"]),
            "FanDiameterMM": int(specs["Średnica wentylatora"].split()[0]),
            "MaxFanSpeedPerMin": int(specs["Maksymalna prędkość obrotowa"].split()[0]) if specs["Maksymalna prędkość obrotowa"] != "Brak danych" else None,
            "MaxNoiseLevelinDBA": float(specs["Maksymalny poziom hałasu"].split()[0]) if specs["Maksymalny poziom hałasu"] != "Brak danych" else None,
            "AirflowCFM": float(specs["Przepływ powietrza [CFM]"]) if specs["Przepływ powietrza [CFM]"] != "Brak danych" else None,
            "LifespanHours": int(specs["Żywotność"].split()[0]) if specs["Żywotność"] != "Brak danych" else None,
        }
    elif second_route == 1:
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "IntelCompatibility": specs["Kompatybilność z procesorami Intel"].replace("\n", ""),
            "AMDCompatibility": specs["Kompatybilność z procesorami AMD"].replace("\n", ""),
            "Lighting": specs["Podświetlenie"] if specs["Podświetlenie"] != "Brak" else None,
            "WeightG": int(specs["Waga [g]"]) if specs["Waga [g]"] != "Brak danych" else None,
            "RadiatorSizeMM": float(specs["Rozmiar chłodnicy"].split()[0]),
            "RadiatorLengthMM": float(specs["Długość chłodnicy [mm]"]),
            "RadiatorWidthMM": float(specs["Szerokość chłodnicy [mm]"]),
            "RadiatorHeightMM": float(specs["Wysokość chłodnicy [mm]"]),
            "FanCount": int(specs["Liczba wentylatorów"]),
            "FanDiameterMM": int(specs["Średnica wentylatora"].split()[0]),
            "MaxFanSpeedRPM": int(specs["Maksymalna prędkość obrotowa"].split()[0]),
            "HasPWMControl": True if specs["Regulacja obrotów PWM"] == "Tak" else False,
            "MaxAirflowCFM": float(specs["Maksymalny przepływ powietrza"].split()[0]) if specs["Maksymalny przepływ powietrza"] != "Brak danych" else None,
            "MaxNoiseLevelDBa": float(specs["Maksymalny poziom hałasu"].split()[0]) if specs["Maksymalny poziom hałasu"] != "Brak danych" else None,
            "FanConnector": specs["Złącze wentylatora"],
            "PumpConnector": specs["Złącze pompy"] if specs["Złącze pompy"] != "Brak danych" else None,
            "LEDConnector": specs["Złącze podświetlenia LED"] if specs["Złącze podświetlenia LED"] not in ["Brak danych", "Nie dotyczy"] else None
        }
    else:
        translated = specs
    return translated


def translate_and_parse_parts(second_route, specs):
    if second_route == 0:  # dyski HDD
        translated = {
            "Name": specs["Nazwa"].replace('"', " cala"),
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Description": None,
            "Type": "HDD",
            "Model": specs["Linia"],
            "FormFactor": specs["Format dysku"].replace('"', ' cala'),
            "Capacity": specs["Pojemność dysku"],
            "Interface": specs["Interfejs"].replace("III", "3").replace("II", "2").replace("I", "1"),
            "ThiccnessMM": float(specs["Grubość [mm]"]) if specs["Grubość [mm]"] != "Brak danych" else None,
            "CacheMemory": specs["Pamięć podręczna"] if specs["Pamięć podręczna"] != "Brak danych" else None,
            "NoiseLevelDB": float(specs["Poziom hałasu"].replace(" dB", "")) if specs["Poziom hałasu"] != "Brak danych" else None,
            "RotatingSpeedRPM": int(specs["Prędkość obrotowa"].split()[0]),
            "WeightG": float(specs["Waga [g]"]) if specs["Waga [g]"] != "Brak danych" else None
        }
    elif second_route == 1:  # dyski SSD
        translated = {
            "Name": specs["Nazwa"].replace('"', " cala"),
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Description": None,
            "Type": "SSD",
            "Model": specs["Model"],
            "FormFactor": specs["Format dysku"].replace('"', ' cala'),
            "Capacity": specs["Pojemność dysku"],
            "Interface": specs["Interfejs"].replace("III", "3").replace("II", "2").replace("I", "1"),
            "ThiccnessMM": float(specs["Grubość"].split()[0]),
            "CacheMemory": None if specs["Pamięć podręczna"] == "Brak danych" else specs["Pamięć podręczna"],
            "Radiator": True if specs["Radiator"] == "Tak" else False,
            "MemoryChipType": None if specs["Rodzaj kości pamięci"] == "Brak danych" else specs["Rodzaj kości pamięci"],
            "ReadSpeedMBs": int(specs["Szybkość odczytu"].split()[0]),
            "WriteSpeedMBs": int(specs["Szybkość zapisu"].split()[0]),
            "ReadRandomIOPS": int(specs["Odczyt losowy"].split()[0]) if specs["Odczyt losowy"] != "Brak danych" else None,
            "WriteRandomIOPS": int(specs["Zapis losowy"].split()[0]) if specs["Zapis losowy"] != "Brak danych" else None,
            "Longevity": specs["Nominalny czas pracy"] if specs["Nominalny czas pracy"] != "Brak danych" else None,
            "TBW": None if specs["TBW (Total Bytes Written)"] == "Brak danych" else specs["TBW (Total Bytes Written)"],
            "Key": specs["Klucz"] if specs["Klucz"] != "Nie dotyczy" else None,
            "Controler": specs["Kontroler"] if specs["Kontroler"] != "Brak danych" else None,
            "HardwareEncryption": True if specs["Szyfrowanie sprzętowe"] == "Tak" else False
        }
    elif second_route == 3:  # karty graficzne
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "ChipsetProducer": specs["Producent chipsetu"],
            "ChipsetType": specs["Rodzaj chipsetu"],
            "CoreClockMHz": int(specs["Taktowanie rdzenia"].split()[0]),
            "BoostClockMHz": int(specs["Taktowanie rdzenia w trybie boost"].split()[0]),
            "StreamProcessors": int(specs["Procesory strumieniowe"]),
            "ROPUnits": int(specs["Jednostki ROP"]),
            "TextureUnits": int(specs["Jednostki teksturujące"]),
            "RTCores": int(specs["Rdzenie RT"]) if specs["Rdzenie RT"] != "Brak" else 0,
            "TensorCores": int(specs["Rdzenie Tensor"]) if specs["Rdzenie Tensor"] != "Brak" else 0,
            "HasDLSS3Support": True if specs["DLSS 3.0"] == "Tak" else False,
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
            "FanCount": int(specs["Ilość wentylatorów"]),
            "DSub": int(specs["D-Sub"]) if specs["D-Sub"] != "Brak" else 0,
            "DisplayPortCount": int(specs["DisplayPort"]) if "DisplayPort" in specs else 0,
            "MiniDisplayPort": int(specs["MiniDisplayPort"]) if specs["MiniDisplayPort"] != "Brak" else 0,
            "DVI": int(specs["DVI"]) if specs["DVI"] != "Brak" else 0,
            "HDMI": int(specs["HDMI"]),
            "USBC": int(specs["USB-C"]) if specs["USB-C"] != "Brak" else 0,
            "PowerConnectors": specs["Złącza zasilania"],
            "Description": None
        }
    elif second_route == 6:  # obudowy
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Color": specs["Kolor"],
            "HasLightning": True if specs["Podświetlenie"] == "Tak" else False,
            "HeightCM": float(specs["Wysokość [cm]"]),
            "LengthCM": float(specs["Głębokość [cm]"]),
            "WidthCM": float(specs["Szerokość [cm]"]),
            "WeightKG": float(specs["Waga [kg]"]) if specs["Waga [kg]"] != "Brak danych" else -1,
            "CaseType": specs["Typ obudowy"],
            "Compatibility": specs["Kompatybilność"].replace("\n", ""),
            "HasWindow": True if specs["Okno"] == "Tak" else False,
            "IsMuted": True if specs["Wyciszona"] == "Tak" else False,
            "MaxGPULengthCM": float(specs["Maksymalna długość karty graficznej [cm]"]) if specs["Maksymalna długość karty graficznej [cm]"] != "Brak danych" else -1,
            "MaxCoolingSystemHeightCM": float(specs["Maksymalna wysokość układu chłodzenia CPU [cm]"]) if specs["Maksymalna wysokość układu chłodzenia CPU [cm]"] != "Brak danych" else -1,
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
            "PowerSupplyPower": specs["Moc zasilacza"] if specs["Moc zasilacza"] != "Brak zasilacza" else None,
            "Description": None
        }
    elif second_route == 8:  # pamięci RAM
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace("zł", "").replace(",", ".").replace(" ", "")),
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
            "VoltageV": float(specs["Napięcie [V]"]),
            "OverclockingProfile": specs["Technologia podkręcania"].replace("\n", ""),
            "Color": specs["Kolor"],
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False
        }
    elif second_route == 9:  # płyty główne
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace("zł", "").replace(",", ".").replace(" ", "")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "BoardStandard": specs["Standard płyty"],
            "WidthMM": float(specs["Szerokość [mm]"]),
            "DepthMM": float(specs["Głębokość [mm]"]),
            "Chipset": specs["Chipset płyty"],
            "CPUSocket": specs["Gniazdo procesora"],
            "SupportedProcessors": specs["Obsługiwane procesory"].replace("\n", ""),
            "RAIDController": specs["Kontroler RAID"].replace("\n", "") if specs["Kontroler RAID"] != "Nie" else None,
            "MemoryStandard": specs["Standard pamięci"],
            "MemoryConnectorType": specs["Rodzaj złącza"],
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
            "IncludedAccessories": specs["Załączone wyposażenie"].replace("\n", "") if specs.get("Załączone wyposażenie") is not None else None,
        }
    elif second_route == 11:  # procesory
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Line": specs["Linia"],
            "HasIncludedCooling": True if specs["Załączone chłodzenie"] == "Tak" else False,
            "SocketType": specs["Typ gniazda"],
            "NumberOfCores": int(specs["Liczba rdzeni"]),
            "NumberOfThreads": int(specs["Liczba wątków"]),
            "ProcessorBaseFrequencyGHz": float(specs["Częstotliwość taktowania procesora"].replace(" GHz", "")),
            "MaxTurboFrequencyGHz": float(specs["Częstotliwość maksymalna Turbo"].replace(" GHz", "")),
            "IntegratedGraphics": None if specs["Zintegrowany układ graficzny"] == "Nie posiada" else specs["Zintegrowany układ graficzny"],
            "HasUnlockedMultiplier": True if specs["Odblokowany mnożnik"] == "Tak" else False,
            "Architecture": specs["Architektura"],
            "ManufacturingProcess": specs["Proces technologiczny"],
            "ProcessorMicroarchitecture": specs["Mikroarchitektura procesora"],
            "TDPinW": int(specs["TDP"].replace(" W", "")),
            "MaxOperatingTempC": int(specs.get("Maksymalna temperatura pracy").replace(" st. C", "")) if specs.get("Maksymalna temperatura pracy") is not None else None,
            "SupportedMemoryTypes": specs["Rodzaje obsługiwanej pamięci"] if specs["Rodzaje obsługiwanej pamięci"] != "Brak danych" else None,
            "L1Cache": specs["Pamięć podręczna L1"].replace("\n", ""),
            "L2Cache": specs["Pamięć podręczna L2"],
            "L3Cache": specs["Pamięć podręczna L3"],
            "AddedEquipment": specs.get("Załączone wyposażenie")
        }
    elif second_route == 12:  # zasilacze
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace("zł", "").replace(" ", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "FormFactor": specs["Standard/Format"],
            "PowerW": int(specs["Moc"].split('W')[0].strip()),
            "Certificate": specs["Certyfikat sprawności"],
            "PowerFactorCorrection": specs["Układ PFC"],
            "EfficiencyRating": specs["Sprawność"],
            "Cooling": specs["Typ chłodzenia"],
            "FanDiameterMM": int(specs["Średnica wentylatora"].split(' ')[0]),
            "Security": specs["Zabezpieczenia"].replace("\n", ""),
            "ModularCabling": specs["Modularne okablowanie"] if specs["Modularne okablowanie"] != "Nie" else None,
            "ATX24Pin_20Plus4": 0 if specs["ATX 24-pin (20+4)"] == "Nie" else int(specs["ATX 24-pin (20+4)"]),
            "PCIE8Pin_6Plus2": 0 if specs["PCI-E 8-pin (6+2)"] == "Nie" else int(specs["PCI-E 8-pin (6+2)"]),
            "PCIE16Pin": 0 if specs["12VHPWR PCI-E 5.0 16-pin (12+4)"] == "Nie" else int(specs["12VHPWR PCI-E 5.0 16-pin (12+4)"]),
            "PCIE8Pin": 0 if specs["PCI-E 8-pin"] == "Nie" else int(specs["PCI-E 8-pin"]),
            "PCIE6Pin": 0 if specs["PCI-E 6-pin"] == "Nie" else int(specs["PCI-E 6-pin"]),
            "CPU8Pin_4Plus4": 0 if specs["CPU 8-pin (4+4)"] == "Nie" else int(specs["CPU 8-pin (4+4)"]),
            "CPU8Pin": 0 if specs["CPU 8-pin"] == "Nie" else int(specs["CPU 8-pin"]),
            "CPU4Pin": 0 if specs["CPU 4-pin"] == "Nie" else int(specs["CPU 4-pin"]),
            "Sata": 0 if specs["SATA"] == "Nie" else int(specs["SATA"]),
            "Molex": 0 if specs["Molex"] == "Nie" else int(specs["Molex"]),
            "HeightMM": int(specs["Wysokość [mm]"]),
            "WidthMM": int(specs["Szerokość [mm]"]),
            "DepthMM": int(specs["Głębokość [mm]"]),
            "HasLighting": True if specs["Podświetlenie"] == "Tak" else False
        }
    else:
        translated = specs
    return translated


def add_products_to_database(first_route, second_route, prods):
    if first_route == 2:  # podzespoly
        if second_route == 0:
            url = 'http://localhost:5198/api/Storage'
        elif second_route == 1:
            url = 'http://localhost:5198/api/Storage'
        elif second_route == 3:
            url = "http://localhost:5198/api/GraphicCard"
        elif second_route == 6:
            url = 'http://localhost:5198/api/Case'
        elif second_route == 8:
            url = 'http://localhost:5198/api/Ram'
        elif second_route == 9:
            url = 'http://localhost:5198/api/Motherboard'
        elif second_route == 11:
            url = 'http://localhost:5198/api/Cpu'
        elif second_route == 12:
            url = 'http://localhost:5198/api/PowerSupply'
        else:
            url = ""
    elif first_route == 0:  # chlodzenie
        if second_route == 0:
            url = "http://localhost:5198/api/CpuCooling"
        elif second_route == 1:
            url = "http://localhost:5198/api/WaterCooling"
        else:
            url = ""
    else:
        url = ""

    if url != "":
        i = 1
        for product in prods:
            response = requests.post(url, json=product)

            if response.status_code == 200:
                print(f"Request {i} was successful.")
            else:
                print(f"Request failed with status code: {response.status_code}")
                print(response.json())
                print(f"Request failed for product: {product}")
                break
            i += 1


def load_up_database():
    mains = [0, 2]
    coolings = [0, 1]
    parts = [0, 1, 3, 6, 8, 9, 11, 12]
    for second_route in coolings:
        prod_links = go_through_route(mains[0], second_route)
        prods = get_product_specs(mains[0], second_route, prod_links)
        add_products_to_database(mains[0], second_route, prods)
    for second_route in parts:
        prod_links = go_through_route(mains[1], second_route)
        prods = get_product_specs(mains[1], second_route, prod_links)
        add_products_to_database(mains[1], second_route, prods)


if fill_database is True:
    load_up_database()
elif main_route in [0, 2] and fill_database is False:
    if main_route == 0:
        product_links = go_through_route(main_route, cooling_route)
        products = get_product_specs(main_route, cooling_route, product_links)
        if add_to_database:
            add_products_to_database(main_route, cooling_route, products)

    elif main_route == 2:
        product_links = go_through_route(main_route, parts_route)
        products = get_product_specs(main_route, parts_route, product_links)
        if add_to_database:
            add_products_to_database(main_route, parts_route, products)