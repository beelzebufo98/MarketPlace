namespace OrdersService.Domain.Entities
{
  public sealed class OrderEntity
  {
    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }

    public int Amount { get; set; }
    public string Description { get; set; }
    public StatusType Type { get; set; }
  }
}
