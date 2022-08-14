FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DDDapp.Api/DDDapp.Api.csproj", "DDDapp.Api/"]
COPY ["DDDapp.Infrastructure/DDDapp.Infrastructure.csproj", "DDDapp.Infrastructure/"]
COPY ["DDDapp.Application/DDDapp.Application.csproj", "DDDapp.Application/"]
COPY ["DDDapp.Domain/DDDapp.Domain.csproj", "DDDapp.Domain/"]
RUN dotnet restore "DDDapp.Api/DDDapp.Api.csproj"
COPY . .
WORKDIR "/src/DDDapp.Api"
RUN dotnet build "DDDapp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDDapp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDDapp.Api.dll"]