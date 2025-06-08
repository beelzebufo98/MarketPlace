using PaymentsService.Domain.Enitities;

namespace PaymentsService.Application.Interfaces
{
  public interface IPaymentsService
  {
    Task CreatePayment(Guid paymentId, int amount);
    Task<int> UpdateBalance(Guid paymentId, int balance);
    Task<UserEntity> PrintBalance(Guid paymentId);
  }
}
