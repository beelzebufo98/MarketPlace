namespace PaymentsService.Domain.Enitities;

public sealed record PaymentResult()
{
    public Guid Id { get; init; }
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
}