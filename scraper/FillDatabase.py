import requests
from bs4 import BeautifulSoup
from requests import exceptions
import configparser
import ParseProduct

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
how_many_pages = int(config.get('Settings', 'how_many_pages'))


def go_through_route(first_route, second_route):
    # zloz main routes
    main_routes = []
    html_main = requests.get("https://www.morele.net/podzespoly-komputerowe/").text
    soup = BeautifulSoup(html_main, "lxml")
    main_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in main_links:
        main_routes.append(link_base + link['href'])

    # przejdz przez main route, zloz routy dla czesci
    html_parts = requests.get(main_routes[first_route]).text
    soup = BeautifulSoup(html_parts, "lxml")
    parts_routes = []

    parts_links = soup.find_all('a', class_='col-xs-12 col-md-6 col-lg-4 col-xl-3')
    for link in parts_links:
        parts_routes.append(link_base + link['href'])

    if first_route == 2:
        # usuniecie z listy po kolei linkow do dyskow hdd z demontazu, dyskow ssd z demontazu, pamieci ram z demontazu
        # oraz tunerow TV, FM, kart wideo
        parts_routes = [link for index, link in enumerate(parts_routes) if index not in [1, 3, 11, 15]]
    elif first_route == 0:
        # usuniecie z listy po kolei linkow do akcesorii chlodzenia wodnego, past termoprzewodzacych i termopadow
        parts_routes = [link for index, link in enumerate(parts_routes) if index not in [1, 3, 4]]
    else:
        parts_routes = []

    prod_links = []

    if parts_routes:
        # wejdz na kazda strone z konkretnymi produktami, zbierz do nich linki
        for page in range(1, how_many_pages+1):
            # uwaga, jesli liczba page jest za duza, to morele zwraca po prostu strone 1, zamiast dac jakis komunikat o bledzie -.-
            html_product_search = requests.get(f"{parts_routes[second_route]},,,,,,,,0,,,,/{page}/").text
            soup = BeautifulSoup(html_product_search, "lxml")
            product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
            # utworz na tej podstawie linki do kazdego produktu dla kazdej strony w liczbie okreslonej w how_many_pages
            for link in product_links_raw:
                prod_links.append(link_base + link['href'])
    print(len(prod_links))
    return prod_links


def get_product_specs(first_route, second_route, prod_links):
    i = 1
    all_products = []
    for link in prod_links:
        try:
            # wejdz do poszczegolnego produktu
            html_product = requests.get(link).text
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
                    translated_product_specs = ParseProduct.parse_parts(second_route, product_specs)
                elif first_route == 0:
                    translated_product_specs = ParseProduct.parse_cooling(second_route, product_specs)
                else:
                    translated_product_specs = {}

                # wyswietl rzeczy po tlumaczeniu
                if show_translated_data_in_console:
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


def add_products_to_database(first_route, second_route, prods):
    url = "http://localhost:5198/api/"
    if first_route == 2:  # podzespoly
        if second_route in [0, 1]:  # dyski hdd lub ssd
            url += 'storage'
        elif second_route == 3:
            url += 'graphic-card'
        elif second_route == 6:
            url += 'case'
        elif second_route == 8:
            url += 'ram'
        elif second_route == 9:
            url += 'motherboard'
        elif second_route == 11:
            url += 'cpu'
        elif second_route == 12:
            url += 'power-supply'
        else:
            url = None
    elif first_route == 0:  # chlodzenie
        if second_route == 0:
            url += "cpu-cooling"
        elif second_route == 1:
            url += "water-cooling"
        else:
            url = None
    else:
        url = None

    if url is not None:
        i = 1
        for product in prods:
            response = requests.post(url, json=product)

            if response.status_code == 200:
                print(f"Request {i} was successful.")
            else:
                print(f"Request failed with status code: {response.status_code}")
                print(response.json())
                print(f"for product: {product}")
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
