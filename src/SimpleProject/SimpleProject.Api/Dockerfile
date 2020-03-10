FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SimpleProject.Api/SimpleProject.Api.csproj", "SimpleProject.Api/"]
COPY ["SimpleProject.Data/SimpleProject.Data.csproj", "SimpleProject.Data/"]
RUN dotnet restore "SimpleProject.Api/SimpleProject.Api.csproj"
COPY . .
WORKDIR "/src/SimpleProject.Api"
RUN dotnet build "SimpleProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleProject.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleProject.Api.dll"]