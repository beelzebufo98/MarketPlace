namespace PaymentsService.Controllers.Models
{
  public sealed record PaymentResponce(
    Guid userId,
    int balance);
}
