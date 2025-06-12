using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Data;

namespace OrdersService.Application;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(
        IServiceProvider serviceProvider,
        IProducer<string, string> producer,
        ILogger<OutboxProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _producer = producer;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
            
            var outboxMessages = await context.OutboxMessages
                .Where(m => !m.Processed)
                .OrderBy(m => m.CreatedAt)
                .Take(100)
                .ToListAsync(stoppingToken);

            foreach (var message in outboxMessages)
            {
                try
                {
                    var kafkaMessage = new Message<string, string>
                    {
                        Key = message.Id.ToString(),
                        Value = message.EventData
                    };
                    await _producer.ProduceAsync("orders-events", kafkaMessage, stoppingToken);
                    message.Processed = true;
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing message = {message.Id}");
                }
            }
            await Task.Delay(5000, stoppingToken);
        }
        
    }
}