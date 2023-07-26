using Confluent.Kafka;
using Kafka.SignalR.Helpers;
using Kafka.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Kafka.SignalR
{
    public sealed class KafkaSignalRHub : Hub, IKafkaSignalRHub
    {
        #region Public Method
        public async Task Subscribe(string topicName)
        {
            ArgumentNullException.ThrowIfNull(nameof(topicName));
            await Groups.AddToGroupAsync(Context.ConnectionId, topicName);
            await Clients.Group(topicName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {topicName}.");
        }

        public async Task UnSubscribe(string topicName)
        {
            ArgumentNullException.ThrowIfNull(nameof(topicName));
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, topicName);
            await Clients.Group(topicName).SendAsync("Send", $"{Context.ConnectionId} has left the group {topicName}.");
        }

        #endregion

    }
}
