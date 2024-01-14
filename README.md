Aby uruchomić projekt, na komputerze musi być zainstalowany zestaw narzędzi programistycznych .NET SDK, dostępny pod poniższym linkiem:

https://dotnet.microsoft.com/en-us/download 

Wymagane jest także środowisko Node.js, dostępne na stronie:

https://nodejs.org/ 

Po zainstalowaniu obu tych zależności, wystarczy uruchomić skrypt run project.bat znajdujący się w głównym folderze projektu.

Skrypt uruchomi najpierw serwer poprzez komendę “dotnet run”, po czym zainstaluje zależności strony klienta (korzystając z komendy “npm install”) i ją uruchomi poleceniem “npm run start”.
