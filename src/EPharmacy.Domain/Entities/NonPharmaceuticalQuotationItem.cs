namespace EPharmacy.Domain.Entities;

public record class NonPharmaceuticalQuotationItem : AuditableEntity
{
    public Guid ID { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double Markup { get; set; }
    public double Total { get; private set; }
    public string? Notes { get; set; }

    public NonPharmaceuticalItem NonPharmaceuticalItem { get; set; } = default!;
    public Quotation Quotation { get; set; } = default!;
}
