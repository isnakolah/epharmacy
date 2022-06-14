using EPharmacy.Application.Identity.Provider.DTO;

namespace EPharmacy.Application.Identity.Provider.Commands.CreatePassword;

public record class CreatePasswordCommand(CreatePasswordDTO CreatePassword) : IRequestWrapper;
