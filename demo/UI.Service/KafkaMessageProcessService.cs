using Kafka.SignalR.Services;

namespace KafkaConsumerService
{
    public class KafkaMessageProcessService : IKafkaMessageProcessService
    {
        public string ProcessMessage(string message)
        {
            return $"updated: {message}";
        }
    }
}
