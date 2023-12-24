import requests


def get_products_from_category(category):
    url = f"http://localhost:5198/api/{category}/scraper"
    response = requests.get(url)
    if response.status_code != 200:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(response.json())
        return []

    category_products = response.json()
    return category_products


def add_products_from_category(products, category):
    url = f"http://localhost:5198/api/{category}"
    for product in products:
        response = requests.post(url, json=product)

        if response.status_code == 200:
            print(f"Request for {product['name']} on {url} was successful.")
        else:
            print(f"Request failed on {url} with status code: {response.status_code}")
            print(f"for product: {product}")
            print(response.json())


def update_product(product, category):
    url = f"http://localhost:5198/api/{category}/price"
    response = requests.put(url, json=product)
    if response.status_code == 200:
        print(f"Request for {product['name']} on {url} was successful.\n")
    else:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(f"for product: {product}")
        print(response.json())


def delete_obsolete_price(price_id):
    url = f"http://localhost:5198/api/shop-price/{price_id}"
    response = requests.delete(url)
    if response.status_code == 200:
        print(f"Request for price id {price_id} on {url} was successful.\n")
    else:
        print(f"Request failed on {url} with status code: {response.status_code}")
        print(f"for price with id: {price_id}")
        print(response.json())
