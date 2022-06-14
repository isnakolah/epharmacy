using EPharmacy.Application.Identity.Commands.DTOs;
using MediatR;

namespace EPharmacy.Application.Identity.Common.Commands.Login;

public sealed record class LoginCommand(LoginRequestDTO Credentials) : IRequest<ServiceResult<LoginResponseDTO>>;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<LoginResponseDTO>>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResult<LoginResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var credentials = request.Credentials;

        var (User, Token) = await _identityService.LoginUserAsync(credentials.Email, credentials.Password);

        return ServiceResult.Success(new LoginResponseDTO(User, Token));
    }
}