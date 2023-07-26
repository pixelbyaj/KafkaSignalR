using Kafka.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Kafka.SignalR
{
    internal class KafkaWorker : BackgroundService
    {
        private readonly IKafkaSignalRService _hubService;
        public KafkaWorker(IKafkaSignalRService hubService)
        {
            _hubService = hubService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                _hubService.StartConsumerAsync(stoppingToken);

            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Int32.MaxValue, stoppingToken);
            }
        }
    }
}
