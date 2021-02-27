FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Agree.Athens.Presentation.WebApi/Agree.Athens.Presentation.WebApi.csproj", "src/Agree.Athens.Presentation.WebApi/"]
RUN dotnet restore "src/Agree.Athens.Presentation.WebApi/Agree.Athens.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Agree.Athens.Presentation.WebApi"
RUN dotnet build "Agree.Athens.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Agree.Athens.Presentation.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agree.Athens.Presentation.WebApi.dll"]
