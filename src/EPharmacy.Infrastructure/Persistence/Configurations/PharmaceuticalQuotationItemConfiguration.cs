using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class PharmaceuticalQuotationItemConfiguration : IEntityTypeConfiguration<PharmaceuticalQuotationItem>
{
    public void Configure(EntityTypeBuilder<PharmaceuticalQuotationItem> builder)
    {
        builder.Property(pharmQuoteItem => pharmQuoteItem.GenericDrug)
            .HasMaxLength(250);

        builder.Property(pharmQuoteItem => pharmQuoteItem.Total)
            .ValueGeneratedOnAddOrUpdate()
            .HasComputedColumnSql("([UnitPrice] * [Quantity]) + [Markup]");
    }
}