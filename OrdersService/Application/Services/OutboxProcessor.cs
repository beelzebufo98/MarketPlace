using Confluent.Kafka;

namespace OrdersService.Application;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<OutboxProcessor> _logger;
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}