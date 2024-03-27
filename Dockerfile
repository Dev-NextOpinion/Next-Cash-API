# Est�gio de compila��o
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Instala��o do gnupg
RUN apt-get update && apt-get install -y gnupg

# Instala��o do SDK do .NET
RUN dotnet --list-sdks

# Copia os arquivos do projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constr�i a aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Est�gio de execu��o
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
