using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
{
    public void Configure(EntityTypeBuilder<Pharmacy> builder)
    {
        builder.Property(pharm => pharm.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(pharm => pharm.Location)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(pharm => pharm.Description)
            .HasDefaultValue(string.Empty);

        builder.ToTable(t => t.IsTemporal());
    }
}