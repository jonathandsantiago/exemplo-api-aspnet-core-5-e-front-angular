using Confluent.Kafka;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Events;
using FavoDeMel.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly ProducerBuilder<Null, string> _producer;
        private readonly MensageriaEvents _mensageriaEvents;

        public MensageriaService(ProducerBuilder<Null, string> producer, MensageriaEvents mensageriaEvents)
        {

            _producer = producer;
            _mensageriaEvents = mensageriaEvents;
        }

        public async Task<object> Publish<T>(T value, string topic)
        {
            return await Publish(JsonConvert.SerializeObject(value), topic);
        }

        public async Task<object> Publish(string value, string topic)
        {
            using var producer = _producer.Build();
            try
            {
                MensagemDto mensagemDto = new MensagemDto(topic, value);
                var sendResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(mensagemDto) });
                var mensagem = new
                {
                    Mensagem = sendResult.Value,
                    sendResult.TopicPartitionOffset
                };
                _mensageriaEvents?.EnviarMensagem(JsonConvert.SerializeObject(mensagem));
                return mensagem;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.HResult}, {ex.Message}");
                throw;
            }
        }
    }
}
