using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Commands.Login;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Identity.Provider.Commands.LoginPharmacyUser;

public sealed record class LoginPharmacyUserCommand(LoginRequestDTO Credentials) : IRequestWrapper<LoginPharmacyUserResponseDTO>;

public sealed class LoginPharmacyUserCommandHandler : IRequestHandlerWrapper<LoginPharmacyUserCommand, LoginPharmacyUserResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _config;
    private readonly IPharmacyUserService _pharmacyUserService;

    public LoginPharmacyUserCommandHandler(IMediator mediator, IApplicationDbContext context, IConfigurationProvider mapperConfig, IUserService userService, IRoleService roleService, IPharmacyUserService pharmacyUserService)
    {
        (_mediator, _context, _config, _userService, _roleService, _pharmacyUserService) = (mediator, context, mapperConfig, userService, roleService, pharmacyUserService); 
    }

    public async Task<ServiceResult<LoginPharmacyUserResponseDTO>> Handle(LoginPharmacyUserCommand request, CancellationToken cancellationToken)
    {
        var userID = await _userService.GetUserIdAsync(request.Credentials.Email);

        var role = await _roleService.GetUserMainRoleAsync(userID);

        if (role is CONCIERGE_AGENT or SYSTEM_ADMIN)
            throw new ForbiddenAccessException();

        var user = await _mediator.Send(new LoginCommand(request.Credentials), cancellationToken);

        var pharmacyID = await _pharmacyUserService.GetUserPharmacyIDAsync(userID);

        var pharmacy = await _context.Pharmacies
            .Where(pharm => pharm.ID == pharmacyID)
            .ProjectTo<GetLessPharmacyDetailsDTO>(_config)
            .FirstOrDefaultAsync(cancellationToken);

        var response = new LoginPharmacyUserResponseDTO(user.Data.User, user.Data.Token, pharmacy);

        return ServiceResult.Success(response);
    }
}