namespace EPharmacy.Domain.Entities;

/// <summary>
/// Table for the registered Pharmacies
/// </summary>
public record class Pharmacy : AuditableEntity
{
    public Guid ID { get; set; }
    public string ConciergeID { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string? Description { get; set; }
    public bool Approved { get; set; }

    public ICollection<PharmacyUser> Users { get; set; } = default!;
    public ICollection<PharmacyPrescription> PharmacyPrescriptions { get; set; } = default!;
    public ICollection<Quotation> Quotations { get; set; } = default!;
}
