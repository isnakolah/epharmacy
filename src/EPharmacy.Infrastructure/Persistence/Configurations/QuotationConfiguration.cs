using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
{
    public void Configure(EntityTypeBuilder<Quotation> builder)
    {
        builder.Property(quote => quote.Status)
            .HasDefaultValue(QuotationStatus.PENDING);

        builder.HasOne(quote => quote.WorkOrder)
            .WithOne(workOrder => workOrder.Quotation)
            .HasForeignKey<WorkOrder>(w => w.ID);

        builder.ToTable(t => t.IsTemporal());
    }
}