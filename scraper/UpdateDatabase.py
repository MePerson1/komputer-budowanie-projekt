import requests
from bs4 import BeautifulSoup
import time
from config import product_categories_and_links, shop_names
from FillDatabase import find_morele_page_limit


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
            "shopName": shop_names["euro.com"],
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
            "shopName": shop_names["komputronik"],
            "link": first_link,
            "price": first_price
        }
        return komputronik
    else:
        print("No valid results from komputronik")
        return {}


# zmienne reprezentujace produkty z bazy danych maja na poczatku db_, a inne nic
def match_morele_products(db_category_products, category_link):
    html_product_search = requests.get(category_link).text
    first_soup = BeautifulSoup(html_product_search, "lxml")

    link_base = "https://www.morele.net"
    page_current = 1
    page_limit = find_morele_page_limit(first_soup)
    matched_products = 0
    all_products = len(db_category_products)
    # slownik stworzony na potrzeby dopasowywania rekordow w czasie O(1)
    category_products_dict = {db_prod["name"]: db_prod for db_prod in db_category_products}
    # produkty, ktore nie maja matcha przeznaczone do sprawdzenia pozniej pod katem przestarzalej ceny
    db_category_products_no_match = db_category_products.copy()

    while matched_products != all_products and page_current != page_limit + 1:
        if page_current != 1:
            html = requests.get(f"{category_link},,,,,,,,0,,,,/{page_current}/").text
            soup = BeautifulSoup(html, "lxml")
        else:
            soup = first_soup

        product_infos = []
        # wyszukiwanie i dodawanie nazw, cen i linkow produktow
        prod_records_raw = soup.find_all('div', class_='cat-product-inside')
        for prod_record in prod_records_raw:
            prod_link = prod_record.find('a', class_='cat-product-image productLink')
            prod_price = prod_record.find('div', class_='price-new').text.replace(" ", "").replace("zł", "").replace("\n", "").replace(",", ".")
            if "od" in prod_price:  # oznacza to, ze dostepne sa tylko opcje outletowe
                continue

            info = {
                "id": 0,
                "name": prod_link['title'].replace('"', ' cala'),
                "shopName": shop_names["morele"],
                "link": link_base + prod_link['href'],
                "price": prod_price
            }
            product_infos.append(info)

        # porownanie wyszukanych produktow z otrzymanymi z bazy i aktualizacja cen
        for info in product_infos:
            db_found_product = category_products_dict.get(info["name"])
            if db_found_product:
                # stworzenie kopii info, aby nie dodac przypadkiem niepoprawnego rekordu
                price = info.copy()
                price.pop("name")

                db_found_product["prices"] = add_or_update_price(db_found_product["prices"], price)
                db_category_products_no_match.remove(db_found_product)

                matched_products += 1

        print(f"Matched products = {matched_products}/{all_products}")
        page_current += 1

    return db_category_products, db_category_products_no_match


def add_or_update_price(price_records, new_price):
    if not new_price:  # jesli cena jest pusta wyjdz od razu
        return price_records

    for record in price_records:  # jesli cena juz istnieje zaktualizuj ja
        if record["shopName"] == new_price["shopName"]:
            record["price"] = new_price["price"]
            record["link"] = new_price["link"]
            return price_records

    price_records.append(new_price)  # jesli ceny jeszcze nie ma, dodaj ja
    return price_records


# aby ta funkcja cos zrobila musza zostac spelnione 4 kroki, dokladnie w takiej kolejnosci
# 1. produkt zostal sciagniety z bazy danych
# 2. zostala sprawdzona cena w sklepie
# 3. okazalo sie ze ceny w sklepie nie ma
# 4. okazalo sie ze w produkcie jest cena z tego sklepu
# wowczas przestarzala cena zostaje wykryta i usunieta
def check_for_obsolete_price(db_product, shop_name):
    for db_price_record in db_product["prices"]:
        if shop_name == db_price_record["shopName"]:
            print(f"Obsolete price found in product {db_product['name']}, shop {db_price_record['shopName']}")
            delete_obsolete_price_in_db(db_price_record["id"])
            db_product["prices"].remove(db_price_record)

    return db_product


def update_product_in_db(db_product, update_link):
    response = requests.put(update_link, json=db_product)
    if response.status_code == 200:
        print(f"Request for {db_product['name']} on {update_link} was successful.\n")
    else:
        print(f"Request failed with status code: {response.status_code}")
        print(response.json())
        print(f"for product: {db_product}")


def delete_obsolete_price_in_db(db_price_id):
    delete_link = f"http://localhost:5198/api/shop-price/{db_price_id}"
    response = requests.delete(delete_link)
    if response.status_code == 200:
        print(f"Request on {delete_link} was successful.\n")
    else:
        print(f"Request failed with status code: {response.status_code}, for link {delete_link}")
        print(response.json())


if __name__ == "__main__":
    # dostowanie slownika z configu do wygladu bardziej odpowiadajacego bazie danych
    database_categories_and_links = product_categories_and_links.copy()
    database_categories_and_links["storage"] = [product_categories_and_links["storage-hdd"], product_categories_and_links["storage-ssd"]]
    database_categories_and_links.pop("storage-hdd")
    database_categories_and_links.pop("storage-ssd")

    # Dodawanie cen po wyszukiwaniu
    for category, link in database_categories_and_links.items():
        print(f"Now scraping for category {category}...")
        category_products_link = f"http://localhost:5198/api/{category}/scraper"
        category_record_update_link = f"http://localhost:5198/api/{category}/price"

        response_data = requests.get(category_products_link)
        category_products = response_data.json()

        # Dodawanie cen masowo, dla morele
        if category == "storage":
            print("hdd")
            category_products, category_products_no_match_hdd = match_morele_products(category_products, link[0])
            print("ssd")
            category_products, category_products_no_match_ssd = match_morele_products(category_products, link[1])

            # zlaczenie dwoch rzekomych grup produktow bez matcha w jeden, ktory faktycznie oddaje stan rzeczy
            category_products_no_match = [
                product for product in category_products_no_match_hdd if product in category_products_no_match_ssd
            ]
        else:
            category_products, category_products_no_match = match_morele_products(category_products, link)

        # sprawdzenie pod katem przestarzalych cen produktow ktore nie mialy zadnego matcha
        for product in category_products_no_match:
            product = check_for_obsolete_price(product, shop_names["morele"])

        # Dodawanie cen dla pojedynczych produktow
        for product in category_products:
            print(f"Scraping prices for {product['name']}...")

            komputronik_price = get_komputronik_price(product["producerCode"])
            if komputronik_price:
                product["prices"] = add_or_update_price(product["prices"], komputronik_price)
            else:
                product = check_for_obsolete_price(product, shop_names["komputronik"])

            euro_com_price = get_euro_com_price(product["producerCode"], product["name"])
            if euro_com_price:
                product["prices"] = add_or_update_price(product["prices"], euro_com_price)
            else:
                product = check_for_obsolete_price(product, shop_names["euro.com"])

            time.sleep(2)  # chwila oddechu dla stron
            if product["prices"]:  # produktow bez cen nie wysylac
                update_product_in_db(product, category_record_update_link)
