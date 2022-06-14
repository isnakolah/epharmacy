using static EPharmacy.Domain.Enums.PrescriptionStatus;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.Ignore(presc => presc.DomainEvents);

        builder.Property(presc => presc.Status)
            .HasDefaultValue(QUOTING);

        builder.Property(presc => presc.FileUrls)
            .HasConversion(
                x => string.Join(",", x),
                y => y.Split(",", StringSplitOptions.None));

        builder.ToTable(t => t.IsTemporal());
    }
}