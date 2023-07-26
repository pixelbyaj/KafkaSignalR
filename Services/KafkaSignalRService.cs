using Confluent.Kafka;
using Kafka.SignalR.Extensions;
using Kafka.SignalR.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    internal class KafkaSignalRService : IKafkaSignalRService
    {
        private IHubContext<KafkaSignalRHub> _hubContext;
        private IKafkaConfigurationService _kafkaConfigurationService;
        public KafkaSignalRService(IHubContext<KafkaSignalRHub> hubContext, IKafkaConfigurationService kafkaConfigurationService)
        {
            _hubContext = hubContext;
            _kafkaConfigurationService = kafkaConfigurationService;
        }


        public async void StartConsumerAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_kafkaConfigurationService.ConsumerConfig).Build();
            string[] topicList = _kafkaConfigurationService.Topics.Split(",");
            consumer.Subscribe(topicList);
            while (true)
            {
                try
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        if (consumeResult == null || consumeResult.IsPartitionEOF)
                        {
                            continue;
                        }
                        await _hubContext.Clients.Group(consumeResult.Topic).SendAsync("RelayMessage", consumeResult.Topic, consumeResult.Message.Value);

                    }
                    catch (ConsumeException e)
                    {
                        await _hubContext.Clients.All.SendAsync("error", e.Message);
                    }
                }
                catch (OperationCanceledException e)
                {
                    await _hubContext.Clients.All.SendAsync("error", e.Message);
                    consumer.Close();
                }
            }
        }

    }
}
