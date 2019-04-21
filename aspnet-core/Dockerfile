FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/MyERP.Web.Host/MyERP.Web.Host.csproj", "src/MyERP.Web.Host/"]
COPY ["src/MyERP.Web.Core/MyERP.Web.Core.csproj", "src/MyERP.Web.Core/"]
COPY ["src/MyERP.EntityFrameworkCore/MyERP.EntityFrameworkCore.csproj", "src/MyERP.EntityFrameworkCore/"]
COPY ["src/MyERP.Common/MyERP.Common.csproj", "src/MyERP.Common/"]
COPY ["src/MyERP.Core/MyERP.Core.csproj", "src/MyERP.Core/"]
COPY ["src/MyERP.Application/MyERP.Application.csproj", "src/MyERP.Application/"]
RUN dotnet restore "src/MyERP.Web.Host/MyERP.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/MyERP.Web.Host"
RUN dotnet build "MyERP.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyERP.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyERP.Web.Host.dll"]