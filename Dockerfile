FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API_Financeiro_Next.csproj", "."]
RUN dotnet restore "./API_Financeiro_Next.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "API_Financeiro_Next.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Financeiro_Next.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


# Adicionando a linha abaixo para criar um volume e armazenar os dados fora do cont�iner
VOLUME /app/ExternalDataProtectionKeys

ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
