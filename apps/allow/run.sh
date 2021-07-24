#!/bin/bash

cd ./src/Agree.Allow.Infrastructure.Data &&

dotnet ef database update &&

cd ../.. &&

cd ./src/Agree.Allow.Presentation &&

dotnet run --configuration Release