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


def go_through_route():
    # zloz main routes
    main_routes = []
    html_main = get("https://www.morele.net/podzespoly-komputerowe/").text
    soup = BeautifulSoup(html_main, "lxml")
    main_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in main_links:
        main_routes.append(link_base + link['href'])

    # przejdz przez main route, zloz routy dla czesci
    html_parts = get(main_routes[main_route]).text
    soup = BeautifulSoup(html_parts, "lxml")
    parts_routes = []

    parts_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in parts_links:
        parts_routes.append(link_base + link['href'])

    # usuniecie z listy po kolei linkow do dyskow hdd z demontazu, dyskow ssd z demontazu, pamieci ram z demontazu
    # oraz tunerow TV, FM, kart wideo
    parts_routes = [link for index, link in enumerate(parts_routes) if index not in [1, 3, 11, 15]]

    # przejdz przez parts route, wejdz na strone z konkretnymi produktami, zbierz do nich linki
    html_processors = get(parts_routes[parts_route]).text
    soup = BeautifulSoup(html_processors, "lxml")
    product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
    prod_links = []

    # utworz na tej podstawie linki do kazdego produktu na pierwszej stronie
    for link in product_links_raw:
        prod_links.append(link_base + link['href'])
    return prod_links


def get_product_specs(prod_links):
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

                translated_product_specs = translate_and_parse_specs(product_specs)

                # wyswietl przetlumaczone rzeczy
                print(f"\nProdukt {i}: {link}")
                for key, value in translated_product_specs.items():
                    print(key, ':', value)

                # dodaj speki produktu do listy ze wszystkimi
                all_products.append(translated_product_specs)
                i += 1
        except exceptions.RequestException as req_error:
            print(f"\n\nRequest error for link {link}: {req_error}\n\n")
        except Exception as e:
            print(f"\n\nAn unexpected error occurred for link {link}: {e}\n\n")
    return all_products


def translate_and_parse_specs(specs):
    if parts_route == 6:
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
            "PanelFront": specs["Panel przedni"],
            "PanelRear": specs["Panel tylny"],
            "PanelSide": specs["Panel boczny"],
            "PanelBottom": specs["Panel dolny"],
            "PanelTop": specs["Panel górny"],
            "PowerSupply": specs["Zasilacz"],
            "PowerSupplyPower": specs["Moc zasilacza"] if specs["Moc zasilacza"] != "Brak zasilacza" else -1,
            "Description": None
        }
    elif parts_route == 11:
        translated = {
            "Name": specs["Nazwa"],
            "Price": float(specs["Cena"].replace(" ", "").replace("zł", "").replace(",", ".")),
            "Producer": specs["Producent"],
            "ProducerCode": specs["Kod producenta"],
            "Line": specs["Linia"],
            "PackagingVersion": specs["Wersja opakowania"],
            "HasIncludedCooling": True if specs["Załączone chłodzenie"] == "Tak" else False,
            "SocketType": specs["Typ gniazda"],
            "NumberOfCores": int(specs["Liczba rdzeni"]),
            "NumberOfThreads": int(specs["Liczba wątków"]),
            "ProcessorBaseFrequencyGHz": float(specs["Częstotliwość taktowania procesora"].replace(" GHz", "")),
            "MaxTurboFrequencyGHz": float(specs["Częstotliwość maksymalna Turbo"].replace(" GHz", "")),
            "IntegratedGraphics": specs["Zintegrowany układ graficzny"],
            "HasUnlockedMultiplier": True if specs["Odblokowany mnożnik"] == "Tak" else False,
            "Architecture": specs["Architektura"],
            "ManufacturingProcess": specs["Proces technologiczny"],
            "ProcessorMicroarchitecture": specs["Mikroarchitektura procesora"],
            "TDPinW": int(specs["TDP"].replace(" W", "")),
            "MaxOperatingTempC": int(specs.get("Maksymalna temperatura pracy").replace(" st. C", "")) if specs.get("Maksymalna temperatura pracy") is not None else -1,
            "SupportedMemoryTypes": specs["Rodzaje obsługiwanej pamięci"],
            "L1Cache": specs["Pamięć podręczna L1"].replace("\n", ""),
            "L2Cache": specs["Pamięć podręczna L2"],
            "L3Cache": specs["Pamięć podręczna L3"],
            "AddedEquipment": specs.get("Załączone wyposażenie")
        }
    else:
        translated = specs
    return translated


def add_products_to_database(prods):
    if parts_route == 6:
        url = 'http://localhost:5198/api/Case'
    elif parts_route == 11:
        url = 'http://localhost:5198/api/Cpu'
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
                print(f"Request failed for product: {product}")
                break
            i += 1


product_links = go_through_route()
products = get_product_specs(product_links)
add_products_to_database(products)
