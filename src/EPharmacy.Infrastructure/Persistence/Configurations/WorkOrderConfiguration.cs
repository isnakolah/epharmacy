using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPharmacy.Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.Property(workorder => workorder.Status)
            .HasDefaultValue(WorkOrderStatus.PENDING);

        builder.Property(workorder => workorder.IsDeleted)
            .HasDefaultValue(false);

        builder.ToTable(t => t.IsTemporal());
    }
}