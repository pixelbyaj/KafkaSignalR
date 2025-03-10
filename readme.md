# Kafka.SignalR

Real-time Kafka Message Delivery to Frontend Using SignalR

[![Nuget](https://img.shields.io/nuget/v/Kafka.SignalR)](https://www.nuget.org/packages/Kafka.SignalR/)
[![Nuget](https://img.shields.io/nuget/dt/Kafka.SignalR)](https://www.nuget.org/packages/Kafka.SignalR/)

## Overview

In this guide, we will demonstrate how to set up a Kafka.SingalR in your application and deliver Kafka messages directly to your frontend client using SignalR. This approach enables real-time updates and seamless communication between your backend and frontend.

## Feature
* Connect Kafka as a Consumer: Establish a connection to Kafka and consume messages from one or more topics.
* Deliver Kafka Messages Directly to UI Client: Use SignalR to push Kafka messages to the frontend in real-time.
* Support for Multiple Kafka Topics: Easily connect and consume messages from multiple Kafka topics.

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

## SignalR Client

```js
const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44363/kafka/hubService").build();

connection.on("RelayMessage", function (topicName,message) {
    // your code 
  
  });

  connection.start().then(function () {
   
   //subscribe to kafka topics
    connection.invoke("Subscribe", "MyTopic");
    connection.invoke("Subscribe", "MyTopic1");
}).catch(function (err) {
    return console.error(err.toString());
});
```
## Override Message Processing (NEW)
If user want to override message processing before delivering it to the SignalR Client. Please follow the below approach.

```C#
public class KafkaMessageProcessService: IKafkaMessageProcessService
{
     public string ProcessMessage(string message)
     {
        string processedMessage = message;
        //Do your process
         return processedMessage;
     }
}

public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddSingleton<IKafkaMessageProcessService, KafkaMessageProcessService>();
    ...

}
```