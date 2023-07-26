using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    internal interface IKafkaConfigurationService
    {
        public void SetKafkaConfiguration(IConfigurationSection configuration);
        public ConsumerConfig? ConsumerConfig { get; }
        public string Topics { get; }

    }
}
