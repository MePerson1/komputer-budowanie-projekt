**Środowisko testowe**

- Scraper:
	- PyCharm Community Edition 2022.1.2
	- Python 3.11
- Serwer (API) 
	- Visual Studio 2022
	- .NET 6.0
- Aplikacja kliencka 
	- Visual Studio Code
	- React 18.2.0
- Baza danych
	- PostgreSQL
- System operacyjny - Windows 10

**Baza danych**

Aplikacja korzysta z systemu PostgreSQL.
W projekcie serwera w pliku appsettings.json należy zmienić wartość KomBuildDBContext, na odpowiednie dane wykorzystywanej bazy danych. 
Przykładowe dane:
Server=localhost;Database=nazwaBazy;User Id=postgres;Password=haslo;

Następnie należy dokonać aktualizacje bazy danych przy pomocy polecenia:
cmd - dotnet ef database update
package manager console - update-database

**Uruchomienie projektu**

Aby uruchomić projekt, na komputerze musi być zainstalowany zestaw narzędzi programistycznych .NET SDK, dostępny pod poniższym linkiem:

https://dotnet.microsoft.com/en-us/download 

Wymagane jest także środowisko Node.js, dostępne na stronie:

https://nodejs.org/ 

Po zainstalowaniu obu tych zależności, wystarczy uruchomić skrypt “run project (windows).bat”, znajdujący się w głównym folderze projektu.

Skrypt uruchomi najpierw serwer poprzez komendę “dotnet run” w folderze z projektem serwera, po czym przejdzie w folder client i zainstaluje zależności strony klienta (korzystając z komendy “npm install”) i ją uruchomi poleceniem “npm run start”.

Web API można uruchomić też przy pomocy Visual Studio 2022
**Scraper**

Aby skorzystać z funkcjonalności scrapera, należy wejść w folder scraper/executables i uruchomić znajdujące się tam pliki z rozszerzeniem “.exe”. Nie należy oddzielać plików wykonywalnych od ich pliku konfiguracyjnego config.ini.

Aby poprawnie wykonało się jakiekolwiek działanie powiązane z bazą danych, serwer musi działać podczas pracy scrapera.

Użytkownik odpowiedzialny za scrapera generowany jest automatycznie przy budowie projektu.
