namespace EPharmacy.Domain.Entities;

public record class Drug
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;
    public string? Manufacturer { get; set; }
    public bool FromDrugIndex { get => string.IsNullOrWhiteSpace(Manufacturer); }

    public ICollection<PharmaceuticalItem> PharmaceuticalItems { get; set; } = default!;
}
