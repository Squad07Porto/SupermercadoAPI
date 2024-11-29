using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Supermercado.API.Services.Interfaces;
using System.Text;

namespace Supermercado.API.Services
{
    using System.Globalization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.DependencyInjection;
    using Supermercado.API.Config.Hubs;
    using Supermercado.API.Models;

    public class RabbitMQService : IRabbitMQService
    {
        private readonly IHubContext<SensorHub> _hubContext;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private const string QueueName = "sensorQueue";

        public RabbitMQService(IServiceScopeFactory scopeFactory, IHubContext<SensorHub> hubContext)
        {
            _hubContext = hubContext;
            
            var factory = new ConnectionFactory() { 
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672")
            };

            factory.AutomaticRecoveryEnabled = true;
            factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);

            _connection = factory.CreateConnection();
            _scopeFactory = scopeFactory;
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Recebido do sensor: " + message);

                using var scope = _scopeFactory.CreateScope();
                var produtoService = scope.ServiceProvider.GetRequiredService<IProdutoService>();
                var secaoService = scope.ServiceProvider.GetRequiredService<ISecaoService>();

                var sectionIdString = message.Split(" ")[^1];

                if (int.TryParse(sectionIdString, out var sectionId))
                {
                    Secao? secao = await secaoService.GetById(sectionId);
                    if (secao == null)
                    {
                        Console.WriteLine($"Seção {sectionId} não encontrada.");
                        return;
                    }

                    IEnumerable<Produto> produtos = await produtoService.GetBySecaoId(secao.Id);

                    if (produtos != null && produtos.Any())
                    {
                        Random random = new();
                        Produto produtoPromocao = produtos.ElementAt(random.Next(produtos.Count()));

                        decimal desconto = (decimal)double.Round(new Random().NextDouble() * (0.60 - 0.10) + 0.10, 2);
                        string precoComDesconto = ConvertToCurrency(produtoPromocao.Preco - produtoPromocao.Preco * desconto);

                        string promocao = 
                            $"Promoção: {produtoPromocao.Nome} com {desconto * 100:0.##}% de desconto! " +
                            $"De {ConvertToCurrency(produtoPromocao.Preco)} " +
                            $"agora por {precoComDesconto} na seção de {secao.Descricao}!";

                        await _hubContext.Clients.All.SendAsync("ReceberMensagem", promocao);
                    }
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
        }

        private static string ConvertToCurrency(decimal value)
        {
            return value.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}