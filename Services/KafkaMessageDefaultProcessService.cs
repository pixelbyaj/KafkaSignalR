using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    internal class KafkaMessageDefaultProcessService : IKafkaMessageProcessService
    {
        public string ProcessMessage(string message)
        {
            return message;
        }
    }
}
