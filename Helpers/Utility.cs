using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.SignalR.Helpers
{
    public static class Utility
    {
        public static IDictionary<string, string>? GetDictionary(IConfigurationSection configurationSection)
        {
            var configs = configurationSection.GetChildren();
            if (configs != null && configs.Any())
            {
                return configs.ToDictionary(x => x.Key, x => x.Value ?? string.Empty);
            }

            return configurationSection.GetChildren()?.ToDictionary(x => x.Key, x => x.Value ?? string.Empty);
        }
    }
}
