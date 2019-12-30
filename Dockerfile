FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 6010
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["src/Futbal.Mng.Api/Futbal.Mng.Api.csproj", "src/Futbal.Mng.Api/"]
COPY ["src/Futbal.Mng.Infrastructure/Futbal.Mng.Infrastructure.csproj", "src/Futbal.Mng.Infrastructure/"]
COPY ["src/Futbal.Mng.Domain/Futbal.Mng.Domain.csproj", "src/Futbal.Mng.Domain/"]
RUN dotnet restore "src/Futbal.Mng.Api/Futbal.Mng.Api.csproj"
COPY . .
WORKDIR "/src/src/Futbal.Mng.Api"
RUN dotnet build "Futbal.Mng.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Futbal.Mng.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Futbal.Mng.Api.dll"]
