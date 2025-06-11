using OrdersService.Application.Interfaces;
using OrdersService.Controllers.Models;
using OrdersService.Domain.Entities;
using OrdersService.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using OrdersService.Domain;

namespace OrdersService.Application.Services
{
  public class OrderService : IOrderService
  {
    private readonly IOrderResultRepository _orderResultRepository;

    public OrderService(IOrderResultRepository orderResultRepository)
    {
      _orderResultRepository = orderResultRepository;
    }
    public async Task CreateOrder(OrdersQueryApi request)
    {
      var order = new OrderEntity
      {
        TaskId = request.TaskId,
        UserId = request.UserId,
        Amount = request.Amount,
        Description = request.Description,
        Type = StatusType.New
      };
      await _orderResultRepository.Add(order);
    }

    public async Task<List<OrderEntity>> GetAllOrders()
    {
      var list = await _orderResultRepository.GetOrders();
      if (list == null)
      {
        throw new Exception();
      }
      return list;
    }

    public async Task<OrderEntity> GetOrder(Guid taskId)
    {
      var result = await _orderResultRepository.GetOrder(taskId);
      if (result == null)
      {
        throw new Exception();
      }
      return result;
    }

    public Task UpdateOrder(OrdersQueryApi request)
    {
      throw new NotImplementedException();
    }
  }
}
