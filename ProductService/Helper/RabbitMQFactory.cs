using RabbitMQ.Client;
using System.Text;

namespace ProductService.Helper
{
    public class RabbitMQFactory
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _queueName;

        public RabbitMQFactory(string rabbitConnection, string queueName)
        {
            var factory = new ConnectionFactory { Uri = new Uri(rabbitConnection) };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _queueName = queueName;
        }

        public async Task PublishMessageAsync(string message)
        {
            try
            {
                await _channel.QueueDeclareAsync(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                await _channel.BasicPublishAsync(exchange: "",
                                        routingKey: _queueName,
                                        body);
                Console.WriteLine($"Mensagem enviada: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao publicar mensagem: {ex.Message}");
            }
        }
    }
}
