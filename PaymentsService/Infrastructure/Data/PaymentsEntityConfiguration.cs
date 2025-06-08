using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsService.Domain.Enitities;

namespace PaymentsService.Infrastructure.Data
{
  public class PaymentsEntityConfiguration : IEntityTypeConfiguration<UserEntity>
  {
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
      builder.HasKey(f => f.Id);
      builder.Property(f => f.Amount);
    }
  }
}
