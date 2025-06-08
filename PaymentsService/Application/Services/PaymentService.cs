using PaymentsService.Application.Interfaces;
using PaymentsService.Domain.Enitities;
using PaymentsService.Infrastructure.Repositories;

namespace PaymentsService.Application.Services
{
  public class PaymentService : IPaymentsService
  {
    private readonly IPaymentResultRepository _resultRepository;

    public PaymentService(IPaymentResultRepository resultRepository)
    {
      _resultRepository = resultRepository;
    }
    public async Task CreatePayment(Guid paymentId, int amount)
    {
      var entity = new UserEntity
      {
        Id = paymentId,
        Amount = amount,
      };
      await _resultRepository.Add(entity);
    }

    public async Task<UserEntity> PrintBalance(Guid paymentId)
    {
      var result = await _resultRepository.GetUser(paymentId);
      if (result == null)
      {
        throw new Exception();
      }
      return result;
    }

    public async Task<int> UpdateBalance(Guid paymentId, int balance)
    {
      var result = await _resultRepository.GetUser(paymentId);
      if (result == null)
      {
        throw new Exception();
      }
      var newEntity = new UserEntity
      {
        Id = paymentId,
        Amount = result.Amount + balance
      };
      await _resultRepository.UpdateBalance(newEntity);
      return newEntity.Amount;
    }
  }
}
