using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [ApiController]
    [Route("Sensor")]
    public class MensagemController(IRabbitMQService rabbitMQService) : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService = rabbitMQService;

        public class SensorDataRequest
        {
            [JsonPropertyName("secaoId")]
            public int SecaoId { get; set; }

            [JsonPropertyName("clienteId")]
            public int ClienteId { get; set; }
        }

        [HttpPost("EnviarMensagem")]
        public IActionResult EnviarMensagemParaCliente([FromBody] SensorDataRequest request)
        {
            string message = $"Cliente {request.ClienteId} passou pela seção {request.SecaoId}";
            _rabbitMQService.SendMessage(message);

            return Ok("Dados do sensor enviados com sucesso.");
        }
    }
}