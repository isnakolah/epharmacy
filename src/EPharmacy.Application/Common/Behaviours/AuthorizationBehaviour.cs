using EPharmacy.Application.Common.Exceptions;
using MediatR;
using System.Reflection;

namespace EPharmacy.Application.Common.Behaviours;

internal sealed class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRoleService _roleService;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IRoleService roleService, IIdentityService identityService)
    {
        (_currentUserService, _roleService, _identityService) = (currentUserService, roleService, identityService);
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes is not null && authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException();

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    var authorized = false;

                    foreach (var role in roles)
                    {
                        var isInRole = await _roleService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                        throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));

            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                    if (!authorized)
                        throw new ForbiddenAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}

