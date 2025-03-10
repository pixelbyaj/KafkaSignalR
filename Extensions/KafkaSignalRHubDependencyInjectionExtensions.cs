using Confluent.Kafka;
using Kafka.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kafka.SignalR.Extensions
{
    public static class KafkaSignalRHubDependencyInjectionExtensions
    {
        //
        // Summary:
        //     Adds Kafka SignalR services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        //
        // Parameters:
        //   services:
        //     The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services
        //     to.
        //
        // Returns:
        //     An Microsoft.AspNetCore.SignalR.ISignalRServerBuilder that can be used to further
        //     configure the SignalR services.
        public static ISignalRServerBuilder AddKafkaSignalR(this IServiceCollection services,IConfigurationSection configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddSingleton<IKafkaSignalRService, KafkaSignalRService>();
            services.TryAddSingleton<IKafkaMessageProcessService, KafkaMessageDefaultProcessService>();
            services.AddSingleton<IKafkaConfigurationService>((sp) =>
            {
                return new KafkaSignalRServerBuilder().SetConfiguration(configuration);
            });
            services.AddHostedService<KafkaWorker>();
            return services.AddSignalR();

        }
    }
}