# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY ./Supermercado.API/*.csproj ./Supermercado.API/
RUN dotnet restore ./Supermercado.API/Supermercado.API.csproj

# Copiar todos os arquivos do projeto
COPY ./Supermercado.API ./Supermercado.API/

# Publicar o projeto em um diretório específico
RUN dotnet publish ./Supermercado.API/Supermercado.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados para o container
COPY --from=build /app/publish .

# Expor a porta utilizada pelo Kestrel
EXPOSE 80

# Comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "Supermercado.API.dll"]
