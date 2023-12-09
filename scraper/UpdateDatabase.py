import requests
from bs4 import BeautifulSoup
import json


def get_euro_com_price(producer_code, product_name):
    product_name_query = product_name.replace(" ", "%20")  # w wyszukiwaniu nie moze byc spacji

    html_euro_com = requests.get(f"https://www.euro.com.pl/search.bhtml?keyword={product_name_query}").text
    soup = BeautifulSoup(html_euro_com, "lxml")

    first_link = soup.find('a', class_="box-medium__link")
    # sprawdzenie czy pierwszy wynik w ogole istnieje
    if not first_link:
        print("No results for euro com")
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
            print(f"for {first_link}, producer_code={first_code}")
            break

    # wyszukiwanie kodu producenta we wszystkich specyfikacjach
    # tak, czasami jest tylko w poczatkowych, czasami tylko we wszystkich, czasami w obu
    if first_code == "":
        first_advanced_specs = soup.find_all("p", class_="technical-attributes__attribute-name")
        first_advanced_specs.reverse()  # kod producenta jest zwykle gdzies na koncu

        for p in first_advanced_specs:
            print(".")
            if p.text == "Kod producenta:":  # zarowno nazwa atrybutu (typu kod producenta) jak i jego wartosc sa w p, ale sama wartosc juz w spanie for some reason
                first_code = p.find_next('span').get_text(strip=True)  # Znajdz nastepny span i daj jego wartosc
                print(f"for {first_link}, producer_code={first_code}")
                break

    # jesli kody producenta z morele i z pierwszego wyniku wyszukiwania z euro.com sie pokrywaja znaczy ze taka rzecz jest na tej stronie i mozna porownywac ich ceny
    if producer_code == first_code:
        first_price = soup.find("span", class_="price-template__large--total").text
        first_price += "." + soup.find("span", class_="price-template__large--decimal").text
        euro_com = {
            "Site": "Euro.com.pl",
            "Link": first_link,
            "Price": first_price
        }
        return euro_com
    else:
        return {}


def get_komputronik_price(producer_code):
    producer_code_query = producer_code.replace(" ", "%20")

    html_komputronik = requests.get(f"https://www.komputronik.pl/search/category/1?query={producer_code_query}").text
    soup = BeautifulSoup(html_komputronik, "lxml")

    first_code = soup.find('div', class_="mb-4 text-xs text-gray-gravel")
    # sprawdzenie czy pierwszy wynik w ogole istnieje
    if not first_code:
        print("No results for komputronik")
        return {}

    first_code = first_code.find_all('p')  # znalezienie dwoch p z kodem systemowym i producenta
    # powinien byc kod systemowy i kod producenta. Jesli dlugosc jest mniejsza niz 2 znaczy ze nie ma kodu producenta, wiec to moze nawet nie byc czesc
    if len(first_code) < 2:
        return {}

    first_code = first_code[1].text  # wyluskanie "Kod producenta: [kod]"
    first_code = first_code.split('[')[-1].split("]")[0]  # usuniecie wszystkiego przed [ i za ]

    # jesli kody producenta z morele i z pierwszego wyniku wyszukiwania z komputronika sie pokrywaja znaczy ze taka rzecz jest na tej stronie i mozna porownywac ich ceny
    if producer_code == first_code:
        first_link = soup.find('h2', class_="font-headline text-lg font-bold leading-6 line-clamp-3 md:text-xl md:leading-8").a.get("href")
        first_price = float(soup.find('div', class_="text-3xl font-bold leading-8").text.replace(" zł", "").replace("\xa0", "").replace(",", "."))
        komputronik = {
            "Site": "Komputronik.pl",
            "Link": first_link,
            "Price": first_price
        }
        return komputronik
    else:
        return {}


def get_morele_price():
    return {}


def update_products(json_data):
    products_data = json.loads(json_data)

    prod_type = products_data["ProductType"]
    prods = products_data["Products"]
    for prod in prods:
        all_product_prices = get_product_prices(prod["ProducerCode"], prod["Name"])

    # tutaj bedzie wysylanie jsona z produktami (lub pojedynczo) na konkretny endpoint w zaleznosci od prod_type


def get_product_prices(producer_code, product_name):
    all_prices = []

    morele = get_morele_price()
    komputronik = get_komputronik_price(producer_code)
    euro_com = get_euro_com_price(producer_code, product_name)

    if morele != {}:
        all_prices.append(morele)
    if komputronik != {}:
        all_prices.append(komputronik)
    if euro_com != {}:
        all_prices.append(euro_com)
    all_prices.sort(key=lambda x: x["Price"])
    return all_prices
