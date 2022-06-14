using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class PharmacyPrescriptionConfiguration : IEntityTypeConfiguration<PharmacyPrescription>
{
    public void Configure(EntityTypeBuilder<PharmacyPrescription> builder)
    {
        builder.HasOne(pharmPresc => pharmPresc.Quotation)
            .WithOne(quote => quote.PharmacyPrescription)
            .HasForeignKey<Quotation>(q => q.ID);
    }
}