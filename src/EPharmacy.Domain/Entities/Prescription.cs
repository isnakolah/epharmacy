using System.Collections.Immutable;
using EPharmacy.Domain.Enums;
using static EPharmacy.Domain.Enums.PrescriptionStatus;

namespace EPharmacy.Domain.Entities;

/// <summary>
/// Prescription model 
/// </summary>
public record Prescription : AuditableEntity, IHasDomainEvent
{
    public Guid ID { get; set; }
    public PrescriptionStatus Status { get; private set; }
    public string? Notes { get; set; }
    public string? DeliveryLocation { get; set; }
    public int NoOfItems { get; set; }
    public IEnumerable<string> FileUrls { get; set; } = default!;

    public Patient Patient { get; set; } = default!;
    public ICollection<PharmacyPrescription> PharmacyPrescriptions { get; set; } = default!;
    public ICollection<PharmaceuticalItem> PharmaceuticalItems { get; set; } = default!;
    public ICollection<NonPharmaceuticalItem> NonPharmaceuticalItems { get; set; } = default!;

    public List<DomainEvent> DomainEvents { get; set; } = default!;

    #region Helper props and class methods

    /// <summary>
    /// Checks if prescription is still pending
    /// </summary>
    public bool IsQuoting => Status == QUOTING;

    /// <summary>
    /// Checks if a prescription is cancelled
    /// </summary>
    public bool IsCancelled => Status == CANCELLED;

    /// <summary>
    /// Checks if the prescription is pending
    /// </summary>
    public bool IsPending => Status == PENDING;

    /// <summary>
    /// Checks if the prescription is Approved
    /// </summary>
    public bool IsApproved => Status == APPROVED;

    /// <summary>
    /// Checks if all the quotations are rejected
    /// </summary>
    public bool AllQuotationsRejected => PharmacyPrescriptions?
        .Count(x => x.Quotation is not { } && x.Quotation!.IsRejected)
        .Equals(PharmacyPrescriptions.Count(x => x.Quotation is not { })) ?? false;

    /// <summary>
    /// Checks to see if a prescription has a pending quotation
    /// </summary>
    public bool HasPendingQuotations =>
        PharmacyPrescriptions?.Any(x => x.Quotation is not { } && x.Quotation!.IsPending) ?? false;

    /// <summary>
    /// Approves a prescription
    /// </summary>
    public void Approve() => Status = APPROVED;

    /// <summary>
    /// Cancels a prescription
    /// </summary>
    public void Cancel() => Status = CANCELLED;

    /// <summary>
    /// Changes the status to pending
    /// </summary>
    public void Pend() => Status = PENDING;

    /// <summary>
    /// Cancel quotations associated with a prescription
    /// </summary>
    public void CancelQuotations() => PharmacyPrescriptions?
        .Where(pharmPresc => pharmPresc.Quotation is not { } && !pharmPresc.Quotation!.IsCancelled)
        .ToList()
        .ForEach(pharmPresc => pharmPresc.Quotation.Cancel());

    #endregion
}