using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrdersService.Infrastructure.Data
{
  public class OrdersDbContext : DbContext
  {
    public virtual DbSet<OrderEntity> Orders { get; set; }

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("OrdersService");

      modelBuilder.ApplyConfiguration(new OrdersEntityConfiguration());
    }
  }
}
