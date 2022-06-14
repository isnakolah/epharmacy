namespace EPharmacy.Application.Users.Ponea.Commands.DeletePharmacyAdmin;

public record DeletePharmacyAdminCommand(Guid PharmacyAdminID) : IRequestWrapper;

public class DeletePharmacyAdminCommandHandler : IRequestHandlerWrapper<DeletePharmacyAdminCommand>
{
    public Task<ServiceResult> Handle(DeletePharmacyAdminCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
