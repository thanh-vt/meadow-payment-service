FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder
LABEL stage=build
WORKDIR /tmp/meadow-payment-service
COPY . .
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS main
MAINTAINER pysga1996
WORKDIR /opt/meadow-payment-service
COPY --from=builder /tmp/meadow-payment-service/Main/bin/Release/net7.0/publish/ ./
COPY ./jetbrains_debugger_agent_20210604.19.0 ./jetbrains_debugger_agent_20210604.19.0 
RUN chmod +x jetbrains_debugger_agent_20210604.19.0 
COPY ./entrypoint.sh /etc/entrypoint.sh
ENTRYPOINT ["sh", "/etc/entrypoint.sh"]
EXPOSE 80 81
