using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class PharmaceuticalItemConfiguration : IEntityTypeConfiguration<PharmaceuticalItem>
{
    public void Configure(EntityTypeBuilder<PharmaceuticalItem> builder)
    {
        builder.Property(pharmItem => pharmItem.Dosage)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(pharmItem => pharmItem.Frequency)
            .HasMaxLength(50)
            .IsRequired();
    }
}