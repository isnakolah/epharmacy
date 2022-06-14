namespace EPharmacy.Domain.Entities;

public record class NonPharmaceuticalItem : AuditableEntity
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
    public string Notes { get; set; } = default!;

    public Category Category { get; set; } = default!;
    public Prescription Prescription { get; set; } = default!;
}
