FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
EXPOSE 8001
ENTRYPOINT ["dotnet", "VueShopServer.Api.dll"]
