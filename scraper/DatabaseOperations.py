import requests


def add_products_from_category(chosen_category, products):
    url = "http://localhost:5198/api/"

    # w aplikacji nie ma podzialu na dyski ssd i hdd
    if 'storage' in chosen_category:
        url += 'storage'
    else:
        url += chosen_category

    for product in products:
        response = requests.post(url, json=product)

        if response.status_code == 200:
            print(f"Request for {product['name']} on {url} was successful.")
        else:
            print(f"Request failed on {url} with status code: {response.status_code}")
            print(f"for product: {product}")
            print(response.json())


def update_product(product, url):
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
