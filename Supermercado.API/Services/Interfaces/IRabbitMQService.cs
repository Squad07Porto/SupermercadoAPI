namespace Supermercado.API.Services.Interfaces
{
    public interface IRabbitMQService
    {
        void SendMessage(string message);
        void StartListening();
    }
}