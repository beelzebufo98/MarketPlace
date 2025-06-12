using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsService.Domain.Enitities;

[Table("outbox_messages_pay")]
public sealed record OutboxMessagePay()
{
    public Guid Id { get; init; }
    public string? EventType { get; init; }
    public string? EventData { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool Processed { get; init; }
}