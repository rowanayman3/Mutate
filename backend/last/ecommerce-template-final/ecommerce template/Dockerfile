#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ecommerce-template-final/ecommerce template/ecommerce template.csproj", "ecommerce-template-final/ecommerce template/"]
RUN dotnet restore "ecommerce-template-final/ecommerce template/ecommerce template.csproj"
COPY . .
WORKDIR "/src/ecommerce-template-final/ecommerce template"
RUN dotnet build "ecommerce template.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ecommerce template.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce template.dll"]