FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS base
WORKDIR /app
EXPOSE 80

COPY . .

FROM base AS final

WORKDIR /app
ENTRYPOINT ["dotnet", "host/MyERP.Web.Host.dll"]