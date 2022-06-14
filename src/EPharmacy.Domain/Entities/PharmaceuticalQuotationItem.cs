namespace EPharmacy.Domain.Entities;

/// <summary>
/// The PharmacyQuotationItem model
/// </summary>
public record PharmaceuticalQuotationItem : AuditableEntity
{
    public Guid ID { get; set; }
    public bool Stocked { get; set; }
    public string? GenericDrug { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double Markup { get; set; }
    public double Total { get; private set; }

    public PharmaceuticalItem PharmaceuticalItem { get; set; } = default!;
    public Quotation Quotation { get; set; } = default!;
}
