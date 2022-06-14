namespace EPharmacy.Domain.Entities;

public record class PharmacyUser : AuditableEntity
{
    public Guid ID { get; set; }
    public Pharmacy Pharmacy { get; set; } = default!;
}
