# Kafka.SignalR

Deliver real-time kafka messages to SignalR Clients

[![Nuget](https://img.shields.io/nuget/v/IntelliTect.AspNetCore.SignalR.SqlServer)](https://www.nuget.org/packages/Kafka.SignalR/)

## Installation
Install using nuget

## Usage

1. Install the `Kafka.SignalR` NuGet package.
2. In `ConfigureServices` in `Startup.cs`, configure with `.AddKafkaSignalR(Configuration.GetSection("Kafka"))`:

Simple configuration:
``` cs
services
    .AddKafkaSignalR(Configuration.GetSection("Kafka"))
```

Simple appsettings.json
```json
"Kafka": {
    "Hub": "kafka/hubService",
    "Topics": "MyTopic",
    "GroupId": "myFirstApp",
    "BootstrapServers": "localhost:9092",
    "SecurityProtocol": "PlainText", //Ssl or PlainText
    "EnableAutoCommit": "true",
    "AutoCommitIntervalMs": "600000",
    //If Ssl than configure below
    "SslCaLocation": "",
    "SslCertificateLocation": "",
    "SslKeyLocation": ""
  },
```