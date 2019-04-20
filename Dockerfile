FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-stretch AS build

RUN mkdir -p /app
WORKDIR /src
COPY . .
RUN dotnet restore "firstapp.csproj"
RUN dotnet build "firstapp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "firstapp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "firstapp.dll"]