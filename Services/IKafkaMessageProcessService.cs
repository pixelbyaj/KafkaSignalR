using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    public interface IKafkaMessageProcessService
    {
        string ProcessMessage(string message);
    }
}
