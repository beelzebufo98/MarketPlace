using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Interfaces;
using OrdersService.Controllers.Models;

namespace OrdersService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
      _orderService = orderService;
      _logger = logger;
    }

    [HttpPost("/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderQueryApi query)
    {
      try
      {
        var request = new OrdersQueryApi
        {
          TaskId = Guid.NewGuid(),
          UserId = query.UserId,
          Amount = query.Amount,
          Description = query.Description,
          Type = query.Type
        };
        await _orderService.CreateOrder(request);
        return Ok(request);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpGet("/get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAll()
    {
      try
      {
        var result = await _orderService.GetAllOrders();
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpGet("get/{taskId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetOrder([FromQuery] Guid taskId)
    {
      try
      {
        var result = await _orderService.GetOrder(taskId);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}
