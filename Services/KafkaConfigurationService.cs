using Confluent.Kafka;
using Kafka.SignalR.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Services
{
    internal class KafkaConfigurationService: IKafkaConfigurationService
    {
        public ConsumerConfig? ConsumerConfig
        {
            get;
            private set;
        }

        public string Topics
        {
            get;
            private set;
        }

        public void SetKafkaConfiguration(IConfigurationSection configuration)
        {
            IDictionary<string, string>? section = Utility.GetDictionary(configuration);
            if (section != null && section.Any())
            {
                ConsumerConfig = new ConsumerConfig
                {
                    GroupId = section["GroupId"],
                    BootstrapServers = section["BootstrapServers"],
                    SecurityProtocol = section["SecurityProtocol"] == "ssl" ? SecurityProtocol.Ssl : SecurityProtocol.Plaintext,
                    SslCaLocation = section.ContainsKey("SslCaLocation") ? section["SslCaLocation"] : string.Empty,
                    SslCertificateLocation = section.ContainsKey("SslCertificateLocation") ? section["SslCertificateLocation"] : string.Empty,
                    SslKeyLocation = section.ContainsKey("SslKeyLocation") ? section["SslKeyLocation"] : string.Empty,
                    EnableAutoCommit = Convert.ToBoolean(section["EnableAutoCommit"]),
                    AutoCommitIntervalMs = Convert.ToInt32(section["AutoCommitIntervalMs"]),
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };
                Topics = section["Topics"];
            }
        }
    }
}
