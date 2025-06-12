using Confluent.Kafka;
using PaymentsService.Domain.Enitities;
using PaymentsService.Infrastructure.Data;

namespace PaymentsService.Application.Services;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(
        IServiceProvider serviceProvider,
        IConsumer<string, string> consumer,
        ILogger<OrderCreatedConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _consumer = consumer;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("orders");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var inboxMessage = new InboxMessagePay()
                {
                    Id = Guid.NewGuid(),
                    EventType = "OrderCreated",
                    EventData = consumeResult.Message.Value,
                    CreatedAt = DateTime
                        .Now // тут лучше что-то по типу received, так как по логике уже готовое значение получаем, а не создаём в моменте, нужен фикс нейминга
                };

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();

                await context.InboxMessages.AddAsync(inboxMessage, stoppingToken);
                await context.SaveChangesAsync(stoppingToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing order created event");
            }
        }
    }
}

//TODO: нужен ещё Inbox и Outbox процессоры и в принципе всё, наверное