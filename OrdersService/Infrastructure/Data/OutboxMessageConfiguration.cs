using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Data;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.EventType);
        builder.Property(f => f.EventData);
        builder.Property(f => f.CreatedAt);
        builder.Property(f => f.Processed);
    }
}