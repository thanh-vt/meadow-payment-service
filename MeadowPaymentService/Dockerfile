FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MeadowPaymentService/MeadowPaymentService.csproj", "MeadowPaymentService/"]
RUN dotnet restore "MeadowPaymentService/MeadowPaymentService.csproj"
COPY . .
WORKDIR "/src/MeadowPaymentService"
RUN dotnet build "MeadowPaymentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeadowPaymentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeadowPaymentService.dll"]
