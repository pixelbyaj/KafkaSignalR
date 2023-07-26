using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Kafka.SignalR.MiddleWare
{
    public sealed class KafkaClientMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _configuration;
        public KafkaClientMiddleware(RequestDelegate next, string configuration)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IKafkaSignalRHub kafkaSignalRHub)
        {
            await _next(context);
        }
    }

    public static class KafkaClientMiddlewareExtensions
    {
        public static IApplicationBuilder UseKafkaConfig(
            this IApplicationBuilder builder, string configurationSectionName)
        {
            return builder.UseMiddleware<KafkaClientMiddleware>(configurationSectionName);
        }
    }
}
