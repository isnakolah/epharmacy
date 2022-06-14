namespace EPharmacy.Domain.Entities;

public record class Category
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<PharmaceuticalItem> PharmaceuticalItems { get; set; } = default!;
    public ICollection<NonPharmaceuticalItem> NonPharmaceuticalItems { get; set; } = default!;
}
