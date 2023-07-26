using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    internal interface IKafkaSignalRService
    {
        public void StartConsumerAsync(CancellationToken stoppingToken);
    }
}
