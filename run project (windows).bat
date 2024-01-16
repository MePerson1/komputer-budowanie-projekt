@echo off

rem Step 1: Navigate to the server directory
cd server\KomputerBudowanieAPI\KomputerBudowanieAPI

rem Step 2: Start server in the first console
start cmd /c dotnet run

rem Step 3: Navigate back to the client directory
cd ..\..\..\client

rem Step 4: Install npm dependencies, then start client in the second console
start cmd /c "npm install && npm run start"
