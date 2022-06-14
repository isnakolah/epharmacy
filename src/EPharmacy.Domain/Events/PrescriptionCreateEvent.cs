using EPharmacy.Domain.Entities;

namespace EPharmacy.Domain.Events;

public sealed record PrescriptionCreatedEvent(in Prescription Prescription) : DomainEvent;