#!/bin/sh

/opt/meadow-payment-service/jetbrains_debugger_agent_20210604.19.0 -port=81 > /opt/meadow-payment-service/debug.log 2>&1 &
dotnet /opt/meadow-payment-service/Main.dll