using EPharmacy.Domain.Enums;
using static EPharmacy.Domain.Enums.WorkOrderStatus;

namespace EPharmacy.Domain.Entities;

/// <summary>
/// WorkOrder model 
/// </summary>
public record class WorkOrder : AuditableEntity
{
    public Guid ID { get; set; }
    public string ConciergeAppointmentID { get; set; } = default!;
    public WorkOrderStatus Status { get; set; }
    public string? Notes { get; set; }

    public Quotation Quotation { get; set; } = default!;

    #region Helper methods and properties
    public bool IsPending => Status.Equals(PENDING);

    public void Dispatch() => Status = DISPATCHED;
    #endregion
}
