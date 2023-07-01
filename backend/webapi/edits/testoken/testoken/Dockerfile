
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY external_end.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet publish "external_end.csproj" -c Release -o /app/publish 
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 5000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "external_end.dll"]
