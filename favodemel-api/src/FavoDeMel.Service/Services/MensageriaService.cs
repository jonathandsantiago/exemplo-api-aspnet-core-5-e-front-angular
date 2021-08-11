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
            using var producer = _producer.Build();
            try
            {
                string json = JsonConvert.SerializeObject(value);
                MensagemDto mensagemDto = new MensagemDto(topic, json);
                var sendResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(mensagemDto) });
                var mensagem = new
                {
                    mensagem = value,
                    topic = topic,
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
