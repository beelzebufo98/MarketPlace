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
      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();
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
