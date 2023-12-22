import requests
from bs4 import BeautifulSoup
from requests import exceptions
from config import *
import ParseProduct


def find_morele_page_limit(soup):
    # ustal ilosc wszystkich stron na podstawie szarego przycisku oznaczajacego ostatnia strone
    # bo morele nie zwraca bledu jak sie poda nieistniejaca ilosc stron, tylko wywala na pierwsza -.-
    page_limit = soup.find("div", class_="pagination-btn-nolink-anchor")
    if page_limit:
        page_limit = int(page_limit.text)
    else:
        page_buttons = soup.find_all("a", class_="pagination-btn")
        if page_buttons:
            page_limit = int(page_buttons[-2].text)  # przedostatni, bo ostatni przycisk to strzalka "nastepna strona" ->
        else:  # jesli ich nie ma znaczy ze jest tylko 1 strona
            page_limit = 1

    return page_limit


def collect_product_links(cat_link):
    link_base = "https://www.morele.net"
    prod_links = []

    # wyszukiwanie i dodawanie linkow z pierwszej strony
    html_product_search = requests.get(cat_link).text
    soup = BeautifulSoup(html_product_search, "lxml")
    product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
    for link in product_links_raw:
        prod_links.append(link_base + link['href'])

    page_limit = find_morele_page_limit(soup)
    if page_limit == 1:
        return prod_links

    # wejdz na kazda nastepna strone z konkretnymi produktami, zbierz do nich linki
    for page_current in range(2, how_many_pages+1):
        html_product_search = requests.get(f"{cat_link},,,,,,,,0,,,,/{page_current}/").text
        soup = BeautifulSoup(html_product_search, "lxml")
        product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
        # utworz na tej podstawie linki do kazdego produktu dla kazdej strony w liczbie okreslonej w how_many_pages
        for link in product_links_raw:
            prod_links.append(link_base + link['href'])
        if page_current == page_limit:
            break
    return prod_links


def get_product_specs(chosen_cat, prod_links):
    i = 1
    all_products = []
    for link in prod_links:
        try:
            # wejdz do poszczegolnego produktu
            html_product = requests.get(link).text
            soup = BeautifulSoup(html_product, "lxml")

            # sprawdz czy jest dostepny zanim w ogole zacznie sie jakas obrobka
            available = soup.find('div', class_='prod-available-items')
            if not available:
                raise ParseProduct.ProductNotAvailable

            # zgarnij wszystkie speci
            name = soup.find('h1', class_='prod-name').text
            price = soup.find('div', class_='product-price').text.strip()

            product_specs = {"Nazwa": name,
                             "Cena": price}
            specs_raw = soup.find_all('div', class_='specification__row')

            for row in specs_raw:
                spec_name = row.find('span', class_='specification__name').text.strip()
                spec_value = row.find('span', class_='specification__value').text.strip()
                product_specs[spec_name] = spec_value

            if "Kod producenta" not in product_specs:
                raise ParseProduct.ImportantSpecNotFound("ProducerCode not found")

            # wyswietl rzeczy przed tlumaczeniem
            if show_raw_data_in_console:
                print(f"\nProdukt {i}: {link}")
                for key, value in product_specs.items():
                    print(key, ':', value)

            translated_product_specs = ParseProduct.parse_parts(chosen_cat, product_specs)

            # wyswietl rzeczy po tlumaczeniu
            if show_translated_data_in_console:
                print(f"\n(PRZETLUMACZONE) Produkt {i}: {link}")
                for key, value in translated_product_specs.items():
                    print(key, ':', value)

            # dodaj speki produktu do listy ze wszystkimi
            all_products.append(translated_product_specs)
            i += 1
        except exceptions.RequestException as req_error:
            print(f"Request error for link {link}: {req_error}")
        except ParseProduct.ProductNotAvailable:
            print(f"Product {link} is not available")
        except ParseProduct.ImportantSpecNotFound as e:
            print(f"Product {link} is missing crucial atribute. {e}")
        except Exception as e:
            print(f"\n\nAn unexpected error occurred for link {link}: {e}\n\n")
    print(f"Proper records after parsing: {len(all_products)}")
    return all_products


def add_products_to_database(chosen_cat, prods):
    url = "http://localhost:5198/api/"

    # w aplikacji nie ma podzialu na dyski ssd i hdd
    if 'storage' in chosen_cat:
        url += 'storage'
    else:
        url += chosen_cat

    i = 1
    for product in prods:
        response = requests.post(url, json=product)

        if response.status_code == 200:
            print(f"Request {i} on {url} was successful.")
        else:
            print(f"Request failed with status code: {response.status_code}")
            print(response.json())
            print(f"for product: {product}")
            break
        i += 1


def scrape_all_categories():
    for prod_cat, cat_link in product_categories_and_links.items():
        print(f"Now scraping {how_many_pages} pages of {prod_cat}...")
        prod_links = collect_product_links(cat_link)
        prods = get_product_specs(prod_cat, prod_links)
        if add_to_database:
            add_products_to_database(prod_cat, prods)


if __name__ == "__main__":
    if choose_all_product_categories:
        scrape_all_categories()
    else:
        if chosen_product_category in product_categories_and_links:
            print(f"Now scraping {how_many_pages} pages of {chosen_product_category}...")
            product_links = collect_product_links(product_categories_and_links[chosen_product_category])
            products = get_product_specs(chosen_product_category, product_links)
            if add_to_database:
                add_products_to_database(chosen_product_category, products)
        else:
            print("Variable chosen_product_category in config.py is not equal to any product_categories_and_links key")
