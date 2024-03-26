# Define a imagem base para o est�gio de compila��o
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia o arquivo do projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constr�i a aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Define a imagem base para o est�gio de execu��o
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Exponha a porta 80 para acessar o aplicativo
EXPOSE 80
EXPOSE 443

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
