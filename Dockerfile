FROM mcr.microsoft.com/dotnet/aspnet:5.0
MAINTAINER pysga1996
WORKDIR /app
COPY ./app/publish/ .
ENTRYPOINT ["dotnet", "./MeadowPaymentService.dll"]
VOLUME /app
EXPOSE 80
EXPOSE 443
