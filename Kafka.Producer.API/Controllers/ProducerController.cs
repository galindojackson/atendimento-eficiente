using System;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Kafka.Producer.API.Domain.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Kafka.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProducerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromQuery] AtendimentoDto atendimento)
        {
            return Created("", SendMessageByKafka(atendimento));
        }



        private string SendMessageByKafka(AtendimentoDto atendimento)
        {
            var config = new ProducerConfig
            {
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                BootstrapServers = $"{_configuration.GetSection("Kafka:BootstrapServers").Value}",
                SaslPassword = $"{_configuration.GetSection("Kafka:SaslPassword").Value}",
                SaslUsername = $"{_configuration.GetSection("Kafka:SaslUsername").Value}",
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var message = JsonConvert.SerializeObject(atendimento);

                    var sendResult = producer
                                        .ProduceAsync("atendimento-virtual", new Message<Null, string> { Value = message })
                                            .GetAwaiter()
                                                .GetResult();

                    return $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return string.Empty;
        }

    }
}