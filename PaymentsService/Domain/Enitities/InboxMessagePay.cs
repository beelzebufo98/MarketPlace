using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsService.Domain.Enitities;

[Table("inbox_messages_pay")]
public sealed record InboxMessagePay()
{
    public Guid Id { get; init; }
    public string? EventType { get; init; }
    public string? EventData { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool Processed { get; init; }
}