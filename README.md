# VueShopServer

simple server for [vue-shop](https://github.com/caoyue/vue-shop)

## run

```shell
dotnet restore
dotnet run -p .\VueShopServer.Api\VueShopServer.Api.csproj
```

## publish

```shell
dotnet restore
dotnet publish -c Release -o publish
```

## docker

```shell
docker-compose up -d
```
