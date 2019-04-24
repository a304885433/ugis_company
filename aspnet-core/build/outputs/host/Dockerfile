FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS base
WORKDIR /app
EXPOSE 80
COPY . .

ENTRYPOINT ["dotnet", "MyERP.Web.Host.dll"]
