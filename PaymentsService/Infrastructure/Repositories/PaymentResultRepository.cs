using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain.Enitities;
using PaymentsService.Infrastructure.Data;
using System.Linq;

namespace PaymentsService.Infrastructure.Repositories
{
  public class PaymentResultRepository : IPaymentResultRepository
  {
    private readonly PaymentsDbContext _context;

    public PaymentResultRepository(PaymentsDbContext context)
    {
      _context = context;
    }
    public async Task Add(UserEntity user)
    {
      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetUser(Guid id)
    {
      return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateBalance(UserEntity user)
    {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
    }
  }
}
