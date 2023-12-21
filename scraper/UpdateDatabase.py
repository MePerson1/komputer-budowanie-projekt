import time

import requests
from bs4 import BeautifulSoup
from config import product_categories_and_links


def get_euro_com_price(producer_code, product_name):
    product_name_query = product_name.replace(" ", "%20")  # w wyszukiwaniu nie moze byc spacji

    html_euro_com = requests.get(f"https://www.euro.com.pl/search.bhtml?keyword={product_name_query}").text
    soup = BeautifulSoup(html_euro_com, "lxml")

    first_link = soup.find('a', class_="box-medium__link")
    # sprawdzenie czy pierwszy wynik w ogole istnieje
    if not first_link:
        print("No valid results from euro com")
        return {}

    first_link = "https://www.euro.com.pl" + first_link.get("href")
    first_code = ""

    # w euro.com kod producenta nie pokazuje sie od razu, trzeba wejsc najpierw w konkretny produkt
    html_first_product = requests.get(f"{first_link}").text
    soup = BeautifulSoup(html_first_product, "lxml")

    # wyszukiwanie kodu producenta w poczatkowych specyfikacjach
    first_basic_specs = soup.find_all("span", class_="text-grey-10")
    for span in first_basic_specs:
        if span.text == "Kod producenta:":
            first_code = span.find_next('span').get_text(strip=True)  # Znajdz nastepny span i daj jego wartosc
            break

    # wyszukiwanie kodu producenta we wszystkich specyfikacjach
    # tak, czasami jest tylko w poczatkowych, czasami tylko we wszystkich, czasami w obu
    if first_code == "":
        first_advanced_specs = soup.find_all("p", class_="technical-attributes__attribute-name")
        first_advanced_specs.reverse()  # kod producenta jest zwykle gdzies na koncu

        for p in first_advanced_specs:
            if p.text == "Kod producenta:":  # zarowno nazwa atrybutu (typu kod producenta) jak i jego wartosc sa w p, ale sama wartosc juz w spanie for some reason
                first_code = p.find_next('span').get_text(strip=True)  # Znajdz nastepny span i daj jego wartosc
                break

    # jesli kody producenta z morele i z pierwszego wyniku wyszukiwania z euro.com sie pokrywaja znaczy ze taka rzecz jest na tej stronie i mozna porownywac ich ceny
    if producer_code == first_code:
        first_price = soup.find("span", class_="price-template__large--total")

        if not first_price:
            print("Match found, but product is unavailable")
            return {}

        first_price = first_price.text
        first_price += "." + soup.find("span", class_="price-template__large--decimal").text
        first_price = first_price.replace(" ", "")
        euro_com = {
            "id": 0,
            "shopName": "Euro.com.pl",
            "link": first_link,
            "price": first_price
        }
        return euro_com
    else:
        print("No valid results from euro com")
        return {}


def get_komputronik_price(producer_code):
    producer_code_query = producer_code.replace(" ", "%20")

    html_komputronik = requests.get(f"https://www.komputronik.pl/search/category/1?query={producer_code_query}").text
    soup = BeautifulSoup(html_komputronik, "lxml")

    first_codes = soup.find('div', class_="mb-4 text-xs text-gray-gravel")
    # sprawdzenie czy pierwszy wynik w ogole istnieje
    if not first_codes:
        print("No valid results from komputronik")
        return {}

    first_codes = first_codes.find_all('p')  # znalezienie dwoch p z kodem systemowym i producenta
    # powinien byc kod systemowy i kod producenta. Jesli dlugosc jest mniejsza niz 2 znaczy ze nie ma kodu producenta, wiec to moze nawet nie byc czesc
    if len(first_codes) < 2:
        print("No valid results from komputronik")
        return {}

    first_code = first_codes[1].text  # wyluskanie "Kod producenta: [kod]"
    first_code = first_code.split('[')[-1].split("]")[0]  # usuniecie wszystkiego przed [ i za ]

    # jesli kody producenta z morele i z pierwszego wyniku wyszukiwania z komputronika sie pokrywaja
    # znaczy ze taka rzecz jest na tej stronie i mozna porownywac ich ceny
    if producer_code == first_code:
        first_link = soup.find('h2', class_="font-headline text-lg font-bold leading-6 line-clamp-3 md:text-xl md:leading-8").a.get("href")
        first_price = float(soup.find('div', class_="text-3xl font-bold leading-8").text.replace(" zł", "").replace("\xa0", "").replace(",", "."))
        komputronik = {
            "id": 0,
            "shopName": "Komputronik.pl",
            "link": first_link,
            "price": first_price
        }
        return komputronik
    else:
        print("No valid results from komputronik")
        return {}


def get_morele_prices():
    return {}


def update_database():
    product_categories = list(product_categories_and_links)
    # product_categories.remove("storage-hdd")
    # product_categories.remove("storage-ssd")
    # product_categories.append("storage")

    for category in product_categories:
        category_data_link = f"http://localhost:5198/api/{category}/scraper"
        category_record_update_link = f"http://localhost:5198/api/{category}/price"

        response = requests.get(category_data_link)
        category_data = response.json()

        for product in category_data:
            print(f"Scraping prices for {product['name']}...")
            komputronik_price = get_komputronik_price(product["producerCode"])
            euro_com_price = get_euro_com_price(product["producerCode"], product["name"])
            time.sleep(2)

            # Dodawanie dwoch latwiejszych (po wyszukiwaniu) cen
            if not product["prices"]:
                if komputronik_price:
                    product["prices"].append(komputronik_price)
                if euro_com_price:
                    product["prices"].append(euro_com_price)
            else:
                for shop_price in product["prices"]:
                    if shop_price["shopName"] == komputronik_price.get("shopName") and shop_price["price"] != komputronik_price.get("price"):
                        print(f"Changed price for product {product['name']}, shop {shop_price['shopName']}, from {shop_price['price']} to {komputronik_price['price']}")
                        shop_price["price"] = komputronik_price["price"]
                        shop_price["link"] = komputronik_price["link"]
                    if shop_price["shopName"] == euro_com_price.get("shopName") and shop_price["price"] != euro_com_price.get("price"):
                        print(f"Changed price for product {product['name']}, shop {shop_price['shopName']}, from {shop_price['price']} to {euro_com_price['price']}")
                        shop_price["price"] = euro_com_price["price"]
                        shop_price["link"] = euro_com_price["link"]

            response = requests.put(category_record_update_link, json=product)

            if response.status_code == 200:
                print(f"Request for {product['name']} on {category_record_update_link} was successful.\n")
            else:
                print(f"Request failed with status code: {response.status_code}")
                print(response.json())
                print(f"for product: {product}")


update_database()
