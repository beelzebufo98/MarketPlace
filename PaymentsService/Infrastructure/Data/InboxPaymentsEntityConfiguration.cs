using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsService.Domain.Enitities;

namespace PaymentsService.Infrastructure.Data;

public class InboxPaymentsEntityConfiguration : IEntityTypeConfiguration<InboxMessagePay>
{
    public void Configure(EntityTypeBuilder<InboxMessagePay> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.EventType);
        builder.Property(f => f.EventData);
        builder.Property(f => f.CreatedAt);
        builder.Property(f => f.Processed);
    }
}