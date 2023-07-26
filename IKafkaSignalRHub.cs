using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR
{
    public interface IKafkaSignalRHub
    {
        Task Subscribe(string topicName);
        Task UnSubscribe(string topicName);
     
    }
}
