using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Consumers
{
    public class KafkaConsumer : IDisposable
    {
        private readonly KafkaSettings _kafkaSettings;
        private IConsumer<Ignore, string> _consumer;

        public KafkaConsumer(KafkaSettings kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
        }

        public async Task Builder()
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = _kafkaSettings.BootstrapServers,
                    GroupId = Guid.NewGuid().ToString(),
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    AllowAutoCreateTopics = true
                };

                _consumer = new ConsumerBuilder<Ignore, string>(config).Build();

                foreach (var topic in _kafkaSettings.Topics)
                {
                    await ConfigureTopics(topic);
                    _consumer.Subscribe(topic);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção: {ex.GetType().FullName} | Mensagem: {ex.Message}");
            }
        }

        private async Task ConfigureTopics(string topic)
        {
            try
            {
                using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _kafkaSettings.BootstrapServers }).Build();
                Metadata metadata = adminClient.GetMetadata(topic, TimeSpan.FromSeconds(120));
                if (metadata == null)
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] { new TopicSpecification { Name = topic, ReplicationFactor = 1, NumPartitions = 1 } });
                }
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }

        public async Task ConnectWebSocket(WebSocket webSocket)
        {
            await ConsumeWebSocket(webSocket);
            await SendWebSocket(webSocket);
        }

        private async Task ConsumeWebSocket(WebSocket webSocket)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                while (true)
                {
                    var cr = _consumer.Consume(cts.Token);
                    if (webSocket.State == WebSocketState.Open)
                    {
                        await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(cr.Message.Value), 0, cr.Message.Value.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                _consumer.Close();
                Console.WriteLine($"Cancelada a execução do Consumer pelo seguinte motivo: {ex.Message}");
            }
        }

        private async Task SendWebSocket(WebSocket webSocket)
        {
            try
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!result.CloseStatus.HasValue)
                {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error websocket: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}