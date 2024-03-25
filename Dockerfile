# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia o arquivo do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constrói a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Configuração da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./


# Configurar o serviço para ouvir na porta 80 (o padrão do nginx)
EXPOSE 80
EXPOSE 443

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]