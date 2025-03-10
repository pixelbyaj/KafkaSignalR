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
        private IKafkaMessageProcessService? _kafkaMessageProcessService;
        public KafkaSignalRService(IHubContext<KafkaSignalRHub> hubContext, IKafkaConfigurationService kafkaConfigurationService, IKafkaMessageProcessService? kafkaMessageProcessService = null)
        {
            _hubContext = hubContext;
            _kafkaConfigurationService = kafkaConfigurationService;
            _kafkaMessageProcessService = kafkaMessageProcessService;
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
                        string processMessage = consumeResult.Message.Value;
                        if (_kafkaMessageProcessService != null)
                        {
                            processMessage = _kafkaMessageProcessService.ProcessMessage(processMessage);
                        }
                        await _hubContext.Clients.Group(consumeResult.Topic).SendAsync("RelayMessage", consumeResult.Topic, processMessage);

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
