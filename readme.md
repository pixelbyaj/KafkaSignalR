# Kafka.SignalR

Deliver real-time kafka messages to SignalR Clients

[![Nuget](https://img.shields.io/nuget/v/Kafka.SignalR)](https://www.nuget.org/packages/Kafka.SignalR/)


## Usage

1. Install the `Kafka.SignalR` NuGet package.
  * .NET CLI
  ```cs
    dotnet add package Kafka.SignalR --version 1.0.0
  ```
  * PackageManager
  ```cs
  Install-Package Kafka.SignalR -Version 1.0.0
  ```

2. In `ConfigureServices` in `Startup.cs`, configure with `.AddKafkaSignalR(Configuration.GetSection("Kafka"))`:
3. In `Configure` in `Startup.cs`, configure with `endpoints.MapHub<KafkaSignalRHub>(Configuration["Kafka:Hub"])`

## Configuration

Simple configuration:
``` cs
services.AddKafkaSignalR(Configuration.GetSection("Kafka"))
```

``` cs
 app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapHub<KafkaSignalRHub>(Configuration["Kafka:Hub"])
                endpoints
                .MapGet("/", async context => { 
                    await context.Response.WriteAsync("Running Kafka Service...."); 
                });
            });
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