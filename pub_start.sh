#!/bin/bash

cd Melin.Server/
dotnet publish --configuration Release

dotnet bin/Release/net8.0/Melin.Server.dll
