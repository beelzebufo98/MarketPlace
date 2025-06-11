using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Entities;
using OrdersService.Infrastructure.Data;

namespace OrdersService.Infrastructure.Repositories
{
  public class OrderResultRepository : IOrderResultRepository
  {
    private readonly OrdersDbContext _context;

    public OrderResultRepository(OrdersDbContext context) 
    {
      _context = context;
    }

    public async Task Add(OrderEntity order)
    {
      var outboxMessage = new OutboxMessage
      {
        Id = Guid.NewGuid(),
        EventType = "OrderCreated",
        EventData = JsonSerializer.Serialize(new 
        {
          OrderId = order.TaskId,
          UserId = order.UserId,
          Amount = order.Amount
        }),
        CreatedAt = DateTime.UtcNow,
      };
      using var transaction = await _context.Database.BeginTransactionAsync();
        
      try
      {
        await _context.Orders.AddAsync(order);
        await _context.OutboxMessages.AddAsync(outboxMessage);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        throw new Exception("Error creating order");
      }
    }

    public async Task<List<OrderEntity>?> GetOrders()
    {
      var orders = await _context.Orders.ToListAsync();
      return orders;
    }

    public async Task<OrderEntity?> GetOrder(Guid taskId)
    {
      var result = await _context.Orders.FirstOrDefaultAsync(o => o.TaskId == taskId);
      return result;
    }

    public async Task Update(OrderEntity order)
    {
      _context.Orders.Update(order);
      await _context.SaveChangesAsync();
    }
  }
}
