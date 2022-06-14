using static EPharmacy.Application.Common.Templates.Templates;
using EPharmacy.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.EventHandlers.PrescriptionCreatedEventHandlers;

public class SendTextMessagesToPharmacyAdminsEventHandler : INotificationHandler<DomainEventNotification<PrescriptionCreatedEvent>>
{
    private readonly INotificationService _notificationMessage;
    private readonly IPharmacyUserService _pharmacyUserService;
    private readonly IApplicationDbContext _context;

    public SendTextMessagesToPharmacyAdminsEventHandler(IApplicationDbContext context, INotificationService notificationService, IPharmacyUserService pharmacyUserService)
    {
        (_context, _notificationMessage, _pharmacyUserService) = (context, notificationService, pharmacyUserService);
    }

    public async Task Handle(DomainEventNotification<PrescriptionCreatedEvent> notification, CancellationToken cancellationToken)
    {
        if (notification.DomainEvent.Prescription is not Prescription prescription)
            return;

        var pharmacyIDs = prescription.PharmacyPrescriptions.Select(x => x.Pharmacy.ID);

        foreach (var pharmacyID in pharmacyIDs)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(pharmacyID);

            var pharmacyAdmin = await _context.PharmacyUsers
                .OrderBy(x => x.CreatedOn)
                .FirstOrDefaultAsync(x => x.Pharmacy == pharmacy);

            var user = await _pharmacyUserService.GetSinglePharmacyUserAsync(pharmacyAdmin.ID);

            var text = TextMessage.Kenyan(user.PhoneNumber, TextMessages.NewPrescription(user.FullName));

            await _notificationMessage.SendTextMessage(text, cancellationToken);
        }
    }
}
