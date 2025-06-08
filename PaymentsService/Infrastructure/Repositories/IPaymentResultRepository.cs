using PaymentsService.Domain.Enitities;

namespace PaymentsService.Infrastructure.Repositories
{
  public interface IPaymentResultRepository
  {
    Task Add(UserEntity user);
    Task UpdateBalance(UserEntity user);
    Task<UserEntity?> GetUser(Guid id);
  }
}
