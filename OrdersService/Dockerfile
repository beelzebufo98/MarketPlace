
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarketplaceSolution/OrdersService.csproj", "OrdersService/"]
RUN dotnet restore "OrdersService/OrdersService.csproj"


COPY . .
WORKDIR "/src/MarketplaceSolution"
RUN dotnet build "OrdersService.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "OrdersService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5188
ENTRYPOINT ["dotnet", "OrdersService.dll"]