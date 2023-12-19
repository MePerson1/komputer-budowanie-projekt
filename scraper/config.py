# Slownik zawierajacy pary
# nazwa kategorii podzespolow: link do pierwszej strony ze wszystkimi wynikami
# nazwy kategorii w kluczu sa takie jak te w api uzywane do dodawania produktu
product_categories_and_links = {
    "case": "https://www.morele.net/kategoria/obudowy-33/",
    "cpu": "https://www.morele.net/kategoria/procesory-45/",
    "cpu-cooling": "https://www.morele.net/kategoria/chlodzenie-cpu-633/",
    "graphic-card": "https://www.morele.net/kategoria/karty-graficzne-12/",
    "motherboard": "https://www.morele.net/kategoria/plyty-glowne-42/",
    "power-supply": "https://www.morele.net/kategoria/zasilacze-komputerowe-61/",
    "ram": "https://www.morele.net/kategoria/pamieci-ram-38/",
    "storage-hdd": "https://www.morele.net/kategoria/dyski-hdd-4/",
    "storage-ssd": "https://www.morele.net/kategoria/dyski-ssd-518/",
    "water-cooling": "https://www.morele.net/kategoria/chlodzenie-wodne-zestawy-662/"
}

# Opcja wyboru kategorii produktu do zescrapowania ze slownika powyzej. Musi byc taka sama jak nazwa klucza
chosen_product_category = "case"

# Opcja (jesli jest na True) sprawia, ze wszystkie produkty z kazdej kategorii zostaja zescrapowane i ew dodane do bazy w zaleznosci od add_to_database.
# Jesli jest na False, zostanie wykonane wyszukiwanie dla tylko jednej kategorii produktu
fill_database = True

# Opcja do dodawania zescrappowanych rekordow do bazy danych. Dostepne opcje to True lub False
add_to_database = False

# Opcje do wyswietlania rekordow podczas ich przetwarzania. Dostepne opcje to True lub False
show_raw_data_in_console = False
show_translated_data_in_console = False

# Opcja do ustawiania, ile stron produktów z morele ma być maksymalnie dodane do bazy.
# Kazda strona przeklada sie na maksymalnie 30 odpowiednich produktow do dodania do bazy (jesli czegos im nie brakuje)
# Podawanie wyzszego numeru stron dla komponentu niz ich jest w samym sklepie sprawia, ze zostana zescrapowane po prostu wszystkie ktore sa
how_many_pages = 5
