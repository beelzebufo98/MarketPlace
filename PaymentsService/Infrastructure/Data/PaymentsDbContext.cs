using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PaymentsService.Infrastructure.Data
{
  public class PaymentsDbContext : DbContext
  {
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<InboxMessagePay> InboxMessages { get; set; }
    public virtual DbSet<OutboxMessagePay> OutboxMessages { get; set; }

    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("PaymentsService");

      modelBuilder.ApplyConfiguration(new PaymentsEntityConfiguration());
      modelBuilder.ApplyConfiguration(new InboxPaymentsEntityConfiguration());
      modelBuilder.ApplyConfiguration(new OutboxPaymentsEntityConfiguration());
    }
  }
}
