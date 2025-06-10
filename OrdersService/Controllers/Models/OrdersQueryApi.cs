using OrdersService.Domain;

namespace OrdersService.Controllers.Models
{
  public sealed record OrdersQueryApi()
  {
    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }

    public int Amount { get; set; }
    public string Description { get; set; }
    public StatusType Type { get; set; }
  }
}
