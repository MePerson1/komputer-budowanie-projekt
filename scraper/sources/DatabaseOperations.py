import requests
import configparser

config = configparser.ConfigParser()
config.read('config.ini')
scraper_credentials = dict(config.items('ScraperCredentials'))


def get_special_token():
    url = "http://localhost:5198/api/user/token"

    response = requests.post(url, json=scraper_credentials)
    if response.status_code != 200:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(response.text)
        return "Nope"

    token = response.json()["token"]
    return token


def get_products_from_category(category, token):
    url = f"http://localhost:5198/api/{category}/scraper"
    headers = {"Authorization": f"Bearer {token}"}

    response = requests.get(url, headers=headers)
    if response.status_code != 200:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(response.text)
        return []

    category_products = response.json()
    return category_products


def add_products_from_category(products, category, token):
    url = f"http://localhost:5198/api/{category}"
    headers = {"Authorization": f"Bearer {token}"}

    products_from_category = get_products_from_category(category, token)
    products_from_category_dict = {prod["producerCode"]: prod for prod in products_from_category}

    for product in products:
        product_already_added = products_from_category_dict.get(product["ProducerCode"])
        if product_already_added:
            print(f"Product {product['Name']} is already in the database")
            continue

        response = requests.post(url, json=product, headers=headers)
        if response.status_code == 200:
            print(f"Request for {product['Name']} on {url} was successful.")
        else:
            print(f"Request failed on {url} with status code: {response.status_code}")
            print(f"for product: {product}")
            print(response.text)


def update_product(product, category, token):
    url = f"http://localhost:5198/api/{category}/price"
    headers = {"Authorization": f"Bearer {token}"}

    response = requests.put(url, json=product, headers=headers)
    if response.status_code == 200:
        print(f"Request for {product['name']} on {url} was successful.\n")
    else:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(f"for product: {product}")
        print(response.text)


def delete_obsolete_price(price_id, token):
    url = f"http://localhost:5198/api/shop-price/{price_id}"
    headers = {"Authorization": f"Bearer {token}"}

    response = requests.delete(url, headers=headers)
    if response.status_code == 200:
        print(f"Request for price id {price_id} on {url} was successful.\n")
    else:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(f"for price with id: {price_id}")
        print(response.text)
