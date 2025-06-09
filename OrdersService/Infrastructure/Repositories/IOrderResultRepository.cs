using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Repositories
{
  public interface IOrderResultRepository
  {
    Task Add(OrderEntity order);
    Task Update(OrderEntity order);
    Task<List<OrderEntity>?> GetOrders();
    Task<OrderEntity?> GetOrder(Guid taskId);
  }
}
