using Confluent.Kafka;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly ProducerBuilder<Null, string> _producer;

        public MensageriaService(ProducerBuilder<Null, string> producer)
        {

            _producer = producer;
        }

        public async Task<object> Publish<T>(T value, string topic)
        {
            return await Publish(topic, JsonConvert.SerializeObject(value));
        }

        public async Task<object> Publish(string topic, string value)
        {
            using var producer = _producer.Build();
            try
            {
                MensagemDto mensagemDto = new MensagemDto(topic, value);
                var sendResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(mensagemDto) });

                return new
                {
                    Mensagem = sendResult.Value,
                    sendResult.TopicPartitionOffset
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.HResult}, {ex.Message}");
                throw;
            }
        }
    }
}
