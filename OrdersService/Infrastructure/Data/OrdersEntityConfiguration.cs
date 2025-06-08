using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Data
{
  public class OrdersEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
  {
      public void Configure(EntityTypeBuilder<OrderEntity> builder)
      {
        builder.HasKey(f => f.TaskId);
        builder.Property(f => f.UserId);
        builder.Property(f => f.Amount);
        builder.Property(f => f.Description);
        builder.Property(f => f.Type);
      }
  }
}
