using EPharmacy.Domain.ValueObjects;

namespace EPharmacy.Domain.Entities;

public record class Patient : AuditableEntity
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime DOB { get; set; }
    public int Age => CalculateAge.GetCurrentAge(DOB);
    public string ConciergeID { get; set; } = default!;

    public ICollection<Prescription>? Prescriptions { get; set; }
}
