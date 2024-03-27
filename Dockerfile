# ESTAGIO 1
# Define a imagem base para o est�gio de compila��o
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Instala��o do gnupg
RUN apt-get update && apt-get install -y gnupg

# Instala��o do SDK do .NET
RUN wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg \
    && mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ \
    && wget -q https://packages.microsoft.com/config/debian/10/prod.list \
    && mv prod.list /etc/apt/sources.list.d/microsoft-prod.list \
    && chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg \
    && chown root:root /etc/apt/sources.list.d/microsoft-prod.list \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
       dotnet-sdk-6.0 \
    && rm -rf /var/lib/apt/lists/* \
    && rm -rf /etc/apt/sources.list.d/microsoft-prod.list \
    && rm -rf /etc/apt/trusted.gpg.d/microsoft.asc.gpg

# Copia o arquivo do projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos e constr�i a aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Exp�e a porta 80 para acessar o aplicativo
EXPOSE 80
EXPOSE 443

# ESTAGIO 2
# Define a imagem base para o est�gio de execu��o
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define o comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
