using EPharmacy.Domain.Enums;
using static EPharmacy.Domain.Enums.QuotationStatus;

namespace EPharmacy.Domain.Entities;

/// <summary>
/// The PharmacyQuotation Model
/// </summary>
public record class Quotation : AuditableEntity
{
    public Guid ID { get; set; }
    public int NoQuoted { get; set; }
    public int ToQuote { get; set; }
    public double DeliveryFee { get; set; }
    public double Markup { get; set; }
    public double Total { get; set; }
    public QuotationStatus Status { get; private set; }

    public PharmacyPrescription PharmacyPrescription { get; set; } = default!;
    public WorkOrder? WorkOrder { get; set; }
    public ICollection<PharmaceuticalQuotationItem> PharmaceuticalQuotationItems { get; set; } = default!;
    public ICollection<NonPharmaceuticalQuotationItem> NonPharmaceuticalQuotationItems { get; set; } = default!;

    #region Helper props and class methods

    /// <summary>
    /// Check if the Quotation is approved
    /// </summary>
    public bool IsApproved => Status.Equals(APPROVED);

    /// <summary>
    /// Check if the Quotation is cancelled
    /// </summary>
    public bool IsCancelled => Status.Equals(CANCELLED);

    /// <summary>
    /// Check if the Quotation is rejected
    /// </summary>
    public bool IsRejected => Status.Equals(REJECTED);

    /// <summary>
    /// Check if the Quotation status is pending
    /// </summary>
    public bool IsPending => Status.Equals(PENDING);

    /// <summary>
    /// Approves a quotation
    /// </summary>
    public void Approve() => Status = APPROVED;

    /// <summary>
    /// Rejects a quotation
    /// </summary>
    public void Reject() => Status = REJECTED;

    /// <summary>
    /// Cancels a quotation
    /// </summary>
    public void Cancel() => Status = CANCELLED;

    #endregion
}
