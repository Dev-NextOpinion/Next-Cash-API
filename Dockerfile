# Estágio de compilação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Instalação do gnupg
RUN apt-get update && apt-get install -y gnupg

# Instalação do SDK do .NET
RUN dotnet --list-sdks

# Copia os arquivos do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constrói a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
