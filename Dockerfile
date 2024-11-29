FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./Supermercado.API/*.csproj ./Supermercado.API/
RUN dotnet restore ./Supermercado.API/Supermercado.API.csproj

COPY ./Supermercado.API ./Supermercado.API/

RUN dotnet publish ./Supermercado.API/Supermercado.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

COPY ./wait-for-it.sh ./wait-for-it.sh
RUN chmod +x ./wait-for-it.sh

EXPOSE 80

ENTRYPOINT ["./wait-for-it.sh", "rabbitmq:5672", "--", "dotnet", "Supermercado.API.dll"]
