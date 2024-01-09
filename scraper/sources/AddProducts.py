import requests
from bs4 import BeautifulSoup
from requests import exceptions
import configparser
import ParseProduct
import DatabaseOperations

# poczatkowe ustalenie niezbednych wartosci z config.ini
config = configparser.ConfigParser()
config.read('config.ini')
product_categories_and_links = dict(config.items('ProductCategoriesAndLinks'))
choose_all_product_categories = config.getboolean('Options', 'choose_all_product_categories')
chosen_product_category = config.get('Options', 'chosen_product_category')
how_many_pages_to_scrap = config.getint('Options', 'how_many_pages_to_scrap')
add_to_database = config.getboolean('Options', 'add_to_database')
show_raw_data_in_console = config.getboolean('Options', 'show_raw_data_in_console')
show_translated_data_in_console = config.getboolean('Options', 'show_translated_data_in_console')


def find_product_page_limit(soup):
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
    for prod_link in product_links_raw:
        prod_links.append(link_base + prod_link['href'])

    page_limit = find_product_page_limit(soup)
    if page_limit == 1:
        return prod_links

    # wejdz na kazda nastepna strone z konkretnymi produktami, zbierz do nich linki
    for page_current in range(2, how_many_pages_to_scrap + 1):
        html_product_search = requests.get(f"{cat_link},,,,,,,,0,,,,/{page_current}/").text
        soup = BeautifulSoup(html_product_search, "lxml")
        product_links_raw = soup.find_all('a', class_='cat-product-image productLink')
        # utworz na tej podstawie linki do kazdego produktu dla kazdej strony w liczbie okreslonej w how_many_pages_to_scrap
        for prod_link in product_links_raw:
            prod_links.append(link_base + prod_link['href'])
        if page_current == page_limit:
            break
    return prod_links


def get_products_from_morele(chosen_cat, prod_links):
    all_products = []
    for prod_link in prod_links:
        try:
            # wejdz do poszczegolnego produktu
            html_product = requests.get(prod_link).text
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
                print(f"\nProduct {product_specs['Nazwa']}: {prod_link}")
                for key, value in product_specs.items():
                    print(key, ':', value)

            translated_product_specs = ParseProduct.parse_parts(chosen_cat, product_specs)

            # wyswietl rzeczy po tlumaczeniu
            if show_translated_data_in_console:
                print(f"\n(TRANSLATED) Product {translated_product_specs['Name']}: {prod_link}")
                for key, value in translated_product_specs.items():
                    print(key, ':', value)

            # dodaj speki produktu do listy ze wszystkimi
            all_products.append(translated_product_specs)
        except exceptions.RequestException as req_error:
            print(f"RequestException error occured for {prod_link}: {req_error}")
        except ParseProduct.ProductNotAvailable:
            print(f"Product {prod_link} is not available")
        except ParseProduct.ImportantSpecNotFound as e:
            print(f"Product {prod_link} is missing crucial atribute. {e}")
        except Exception as e:
            print(f"\n\nAn unexpected error occurred for {prod_link}: {e}\n\n")
    print(f"Proper records after parsing: {len(all_products)}")
    return all_products


def scrape_product_category(product_category, category_link):
    print(f"Now scraping {how_many_pages_to_scrap} pages of {product_category}...")
    product_links = collect_product_links(category_link)
    products = get_products_from_morele(product_category, product_links)
    if add_to_database:
        token = DatabaseOperations.get_special_token()
        DatabaseOperations.add_products_from_category(products, product_category, token)


if __name__ == "__main__":
    if choose_all_product_categories:
        for category, link in product_categories_and_links.items():
            scrape_product_category(category, link)
    else:
        if chosen_product_category in product_categories_and_links:
            scrape_product_category(chosen_product_category, product_categories_and_links[chosen_product_category])
        else:
            print("Variable chosen_product_category in config.ini is not equal to any product_categories_and_links key")