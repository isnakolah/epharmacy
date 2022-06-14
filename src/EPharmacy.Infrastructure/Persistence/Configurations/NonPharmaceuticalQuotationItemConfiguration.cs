using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class NonPharmaceuticalQuotationItemConfiguration : IEntityTypeConfiguration<NonPharmaceuticalQuotationItem>
{
    public void Configure(EntityTypeBuilder<NonPharmaceuticalQuotationItem> builder)
    {
        builder.Property(pharmQuoteItem => pharmQuoteItem.Total)
            .ValueGeneratedOnAddOrUpdate()
            .HasComputedColumnSql("([UnitPrice] * [Quantity]) + [Markup]");
    }
}