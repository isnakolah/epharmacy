namespace EPharmacy.Domain.Entities;

/// <summary>
/// PrescriptionItem Class with the implementations of the prescriptionItem
/// </summary>
public record class PharmaceuticalItem : AuditableEntity
{
    public Guid ID { get; set; }
    public string Dosage { get; set; } = default!;
    public string Duration { get; set; } = default!;
    public string Frequency { get; set; } = default!;
    public string? Notes { get; set; }

    public Drug Drug { get; set; } = default!;
    public Formulation Formulation { get; set; } = default!;
    public Prescription Prescription { get; set; } = default!;
    public ICollection<PharmaceuticalQuotationItem> QuotationItems { get; set; } = default!;
}
