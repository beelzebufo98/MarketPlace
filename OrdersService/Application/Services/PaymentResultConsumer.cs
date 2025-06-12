using System.Text.Json;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using OrdersService.Domain;
using OrdersService.Infrastructure.Data;
using PaymentsService.Domain.Enitities;

namespace OrdersService.Application.Services;

public class PaymentResultConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<PaymentResultConsumer> _logger;

    public PaymentResultConsumer(IServiceProvider serviceProvider, IConsumer<string, string> consumer,
        ILogger<PaymentResultConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _consumer = consumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("payments");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var result = JsonSerializer.Deserialize<PaymentResult>(consumeResult.Message.Value);
                if (result == null)
                {
                    throw new Exception();
                }
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
                var order = await context.Orders.FirstOrDefaultAsync(o => o.TaskId == result.Id, stoppingToken);
                if (order != null)
                {
                    order.Type = result.IsSuccess ? StatusType.Finished : StatusType.Cancelled;
                    await context.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while consuming payment result");
            }
        }
    }
}