using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WorkOrderService.Helper;

namespace WorkOrderService.HostRabbitMQ
{
    public class OrdersConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string Queue = Util.GetEnvironmentVariable("PRODUCT_QUEUE");
        private readonly string RabbitConnection = Util.GetEnvironmentVariable("RABBIT_CONNECTION");
        public OrdersConsumer()
        {
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

                await ProcessMessageAsync(message); // chamar service para salvar a order 

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
            await Task.Delay(500);
            Console.WriteLine($"Processando: {message}");
        }
    }
}