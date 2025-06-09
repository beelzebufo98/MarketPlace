using OrdersService.Domain.Entities;

namespace OrdersService.Application.Interfaces
{
  public interface IOrderService
  {
    Task CreateOrder(Guid taskId); // доработать

    Task GetAllOrders();

    Task<OrderEntity> GetOrder(Guid taskId); // dto ввести вместо использования Entity
  }
}
