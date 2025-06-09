using OrdersService.Application.Interfaces;
using OrdersService.Domain.Entities;

namespace OrdersService.Application.Services
{
  public class OrderService : IOrderService
  {
    public Task CreateOrder(Guid taskId)
    {
      throw new NotImplementedException();
    }

    public Task GetAllOrders()
    {
      throw new NotImplementedException();
    }

    public Task<OrderEntity> GetOrder(Guid taskId)
    {
      throw new NotImplementedException();
    }
  }
}
