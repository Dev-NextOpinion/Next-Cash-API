# ESTAGIO 1
# Define a imagem base para o estágio de compilação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Exponha a porta 80 para acessar o aplicativo
EXPOSE 80
EXPOSE 443

# ESTÁGIO 2
# Copia o arquivo do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# ESTÁGIO 3
# Copia o resto dos arquivos e constrói a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# ESTÁGIO 4
# Define a imagem base para o estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./


# ESTÁGIO 5
# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
