using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersService.Domain.Entities;

[Table("OutboxMessages", Schema = "OrdersService")]
public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string EventType { get; set; }
    public string EventData { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Processed { get; set; }
}