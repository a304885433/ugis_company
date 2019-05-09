FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/MyERP.Web.Host/MyERP.Web.Host.csproj", "src/MyERP.Web.Host/"]
COPY ["src/MyERP.Web.Core/MyERP.Web.Core.csproj", "src/MyERP.Web.Core/"]
COPY ["aspnetboilerplate/src/Abp.AspNetCore/Abp.AspNetCore.csproj", "aspnetboilerplate/src/Abp.AspNetCore/"]
COPY ["aspnetboilerplate/src/Abp.Web.Common/Abp.Web.Common.csproj", "aspnetboilerplate/src/Abp.Web.Common/"]
COPY ["aspnetboilerplate/src/Abp/Abp.csproj", "aspnetboilerplate/src/Abp/"]
COPY ["src/MyERP.EntityFrameworkCore/MyERP.EntityFrameworkCore.csproj", "src/MyERP.EntityFrameworkCore/"]
COPY ["src/MyERP.Common/MyERP.Common.csproj", "src/MyERP.Common/"]
COPY ["src/MyERP.Core/MyERP.Core.csproj", "src/MyERP.Core/"]
COPY ["aspnetboilerplate/src/Abp.AutoMapper/Abp.AutoMapper.csproj", "aspnetboilerplate/src/Abp.AutoMapper/"]
COPY ["aspnetboilerplate/src/Abp.ZeroCore.EntityFrameworkCore/Abp.ZeroCore.EntityFrameworkCore.csproj", "aspnetboilerplate/src/Abp.ZeroCore.EntityFrameworkCore/"]
COPY ["aspnetboilerplate/src/Abp.ZeroCore/Abp.ZeroCore.csproj", "aspnetboilerplate/src/Abp.ZeroCore/"]
COPY ["aspnetboilerplate/src/Abp.Zero.Common/Abp.Zero.Common.csproj", "aspnetboilerplate/src/Abp.Zero.Common/"]
COPY ["aspnetboilerplate/src/Abp.EntityFrameworkCore/Abp.EntityFrameworkCore.csproj", "aspnetboilerplate/src/Abp.EntityFrameworkCore/"]
COPY ["aspnetboilerplate/src/Abp.EntityFramework.Common/Abp.EntityFramework.Common.csproj", "aspnetboilerplate/src/Abp.EntityFramework.Common/"]
COPY ["src/MyERP.Application/MyERP.Application.csproj", "src/MyERP.Application/"]
COPY ["aspnetboilerplate/src/Abp.AspNetCore.SignalR/Abp.AspNetCore.SignalR.csproj", "aspnetboilerplate/src/Abp.AspNetCore.SignalR/"]
COPY ["aspnetboilerplate/src/Abp.Castle.Log4Net/Abp.Castle.Log4Net.csproj", "aspnetboilerplate/src/Abp.Castle.Log4Net/"]
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