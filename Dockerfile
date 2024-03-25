# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia o arquivo do projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constr�i a aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Configura��o da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./


# Configurar o servi�o para ouvir na porta 80 (o padr�o do nginx)
EXPOSE 80
EXPOSE 443

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]