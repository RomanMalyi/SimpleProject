FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SimpleProject.Gateway/SimpleProject.Gateway.csproj", "SimpleProject.Gateway/"]
COPY ["SimpleProject.Data/SimpleProject.Data.csproj", "SimpleProject.Data/"]
RUN dotnet restore "SimpleProject.Gateway/SimpleProject.Gateway.csproj"
COPY . .
WORKDIR "/src/SimpleProject.Gateway"
RUN dotnet build "SimpleProject.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleProject.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleProject.Gateway.dll"]