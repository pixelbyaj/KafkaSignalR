using Kafka.SignalR.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Extensions
{
    internal sealed class KafkaSignalRServerBuilder : IKafkaSignalRServerBuilder
    {
        private IKafkaConfigurationService _kafkaConfigurationService;

        public KafkaSignalRServerBuilder()
        {
            _kafkaConfigurationService = new KafkaConfigurationService();
        }

        public IKafkaConfigurationService SetConfiguration(IConfigurationSection configuration)
        {
            _kafkaConfigurationService.SetKafkaConfiguration(configuration);
            return _kafkaConfigurationService;
        }
    }
}
