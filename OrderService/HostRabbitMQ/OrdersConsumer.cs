using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using OrderService.Business.Interfaces;
using Helper;

namespace OrderService.HostRabbitMQ
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            try
            {
                _serviceScopeFactory = serviceScopeFactory;
                var factory = new ConnectionFactory { Uri = new Uri(Util.RabbitConnection) };


                _connection = factory.CreateConnectionAsync().Result;
                _channel = _connection.CreateChannelAsync().Result;

                _channel.QueueDeclareAsync(queue: Util.QueueProduct,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro OrderConsumer Contrutor {e.Message}");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(_channel);

                Console.WriteLine($"Escutando na fila {Util.QueueProduct}");
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] Mensagem recebida: {message}");

                    await ProcessMessageAsync(message);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                };

                await _channel.BasicConsumeAsync(queue: Util.QueueProduct,
                                        autoAck: false,
                                        consumer: consumer);

                Console.WriteLine($"Aguardando Mensagem... ");

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ExecuteAsync OrderConsumer {e.Message}");
            }
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