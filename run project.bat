@echo off

rem Step 1: Navigate to the server directory
cd server\KomputerBudowanieAPI\KomputerBudowanieAPI

rem Step 2: Run "dotnet run" in the first console
start cmd /k dotnet run

rem Step 3: Navigate back to the client directory
cd ..\..\..\client

rem Step 4: Run "npm run start" in the second console
start cmd /k npm run start
