#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["API_Financeiro_Next.csproj", "."]
#RUN dotnet restore "./API_Financeiro_Next.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "API_Financeiro_Next.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "API_Financeiro_Next.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
#WORKDIR /app
#COPY --from=publish /app/publish .
#COPY --from=publish /app/ExternalDataProtectionKeys /app/ExternalDataProtectionKeys
#
## Expor somente a porta 443
#EXPOSE 443
#EXPOSE 80
#
#
#ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]

#

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["API_Financeiro_Next.csproj", "."]
RUN dotnet restore "./API_Financeiro_Next.csproj"

COPY . .

WORKDIR "/src/."
RUN dotnet build "API_Financeiro_Next.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish

WORKDIR /app

COPY --from=build /app/publish .

COPY --from=build /app/ExternalDataProtectionKeys /app/ExternalDataProtectionKeys

# Expose only port 443
EXPOSE 443

# Expose port 80 for potential reverse proxy setup
EXPOSE 80

ENTRYPOINT ["dotnet", "API_Financeiro_Next.dll"]
