using OrdersService.Domain;

namespace OrdersService.Controllers.Models
{
  public sealed record CreateOrderQueryApi()
  {
    public Guid UserId { get; set; }

    public int Amount { get; set; }
    public string Description { get; set; }
    public StatusType Type { get; set; }
  }
}
