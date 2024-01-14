#!/bin/bash

# Step 1: Navigate to the server directory
cd server/KomputerBudowanieAPI/KomputerBudowanieAPI

# Step 2: Start server in the first terminal
gnome-terminal -- dotnet run

# Step 3: Navigate back to the client directory
cd ../../../client

# Step 4: Install npm dependencies and then start client in the second terminal
gnome-terminal -- bash -c "npm install && npm run start"