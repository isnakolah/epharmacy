using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Patients.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using static EPharmacy.Application.Common.Templates.Templates;

namespace EPharmacy.Application.ExternalServices.Ponea.Commands.SendDispatchMessageToPatient;

public sealed record SendDispatchMessageToPatientCommand(Guid PatientID) : IRequestWrapper;

public sealed class SendDispatchMessageToPatientCommandHandler : IRequestHandlerWrapper<SendDispatchMessageToPatientCommand>
{
    private readonly INotificationService _notificationMessage;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly IApplicationDbContext _context;

    public SendDispatchMessageToPatientCommandHandler(INotificationService notificationMessage, IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_notificationMessage, _context, _mapperConfig) = (notificationMessage, context, mapperConfig);
    }

    public async Task<ServiceResult> Handle(SendDispatchMessageToPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Where(patient => patient.ID == request.PatientID)
            .ProjectTo<GetLessPatientDetailsDTO>(_mapperConfig)
            .FirstOrDefaultAsync(cancellationToken);

        var text = TextMessage.Kenyan(patient.Phone, TextMessages.Dispatch(patient.Name));

        await _notificationMessage.SendTextMessage(text, cancellationToken);

        throw new NotImplementedException();
    }
}
