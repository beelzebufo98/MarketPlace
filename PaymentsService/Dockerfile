
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarketplaceSolution/PaymentsService.csproj", "PaymentsService/"]
RUN dotnet restore "PaymentsService/PaymentsService.csproj"


COPY . .
WORKDIR "/src/MarketplaceSolution"
RUN dotnet build "PaymentsService.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "PaymentsService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5105
ENTRYPOINT ["dotnet", "PaymentsService.dll"]