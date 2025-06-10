using OrdersService.Controllers.Models;
using OrdersService.Domain.Entities;

namespace OrdersService.Application.Interfaces
{
  public interface IOrderService
  {
    Task CreateOrder(OrdersQueryApi request);

    Task<List<OrderEntity>> GetAllOrders();

    Task UpdateOrder(OrdersQueryApi request);
    Task<OrderEntity> GetOrder(Guid taskId);
  }
}
