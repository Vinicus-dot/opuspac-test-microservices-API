using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using OrderService.Business.Implements;
using OrderService.Business.Interfaces;
using OrderService.Helper;

namespace OrderService.HostRabbitMQ
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string Queue = Util.GetEnvironmentVariable("PRODUCT_QUEUE");
        private readonly string RabbitConnection = Util.GetEnvironmentVariable("RABBIT_CONNECTION");

        public OrderConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var factory = new ConnectionFactory { Uri = new Uri(RabbitConnection) };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.QueueDeclareAsync(queue: Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            Console.WriteLine($"Escutando na fila {Queue}");
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Mensagem recebida: {message}");

                await ProcessMessageAsync(message);

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(queue: Queue,
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine($"Aguardando Mensagem... ");

            await Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(string message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var orderBusiness = scope.ServiceProvider.GetRequiredService<IOrdersBusiness>();
                await orderBusiness.CreateOrder(new() { Message = message });
                Console.WriteLine($"Processando: {message}");
            }
        }
    }
}