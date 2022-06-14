namespace EPharmacy.Domain.Common;

public abstract record class AuditableEntity
{
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}