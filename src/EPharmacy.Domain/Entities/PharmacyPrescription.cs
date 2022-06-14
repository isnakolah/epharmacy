namespace EPharmacy.Domain.Entities;

public record PharmacyPrescription : AuditableEntity
{
    public Guid ID { get; set; }
    public DateTime Expiry { get; set; }


    public Prescription Prescription { get; set; } = default!;
    public Quotation Quotation { get; set; } = default!;
    public Pharmacy Pharmacy { get; set; } = default!;

    #region Helper Methods
    /// <summary>
    /// Checks if a quotation is already quoted
    /// </summary>
    public bool IsQuoted => Quotation is not {};
    #endregion
}
