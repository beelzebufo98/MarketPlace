namespace PaymentsService.Controllers.Models
{
  public sealed record PaymentsQueryApi(
    Guid userId,
    int amount);
}
