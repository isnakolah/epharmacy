using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Infrastructure.Identity.Extensions;
using EPharmacy.Infrastructure.Identity.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Infrastructure.Identity.Services;

internal sealed class PharmacyUserService : IPharmacyUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IRoleService _roleService;

    public PharmacyUserService(UserManager<ApplicationUser> userManager, IApplicationDbContext context, IRoleService roleService)
    {
        (_userManager, _context, _roleService) = (userManager, context, roleService);
    }

    public async Task<ApplicationUserDTO[]> GetAllPharmacyAdminUsersAsync()
    {
        var role = PHARMACY_ADMIN;

        var users = await _userManager.GetUsersInRoleAsync(role);

        var response = users.Select(user => user.ToDTO(role)).ToArray();

        return response;
    }

    public async Task<Guid> GetUserPharmacyIDAsync(string userID)
    {
        var pharmUser = await _context.PharmacyUsers
            .Include(pharmUser => pharmUser.Pharmacy)
            .Where(pharmUser => pharmUser.ID == Guid.Parse(userID) && pharmUser.Pharmacy != null)
            .FirstOrDefaultAsync();

        if (pharmUser is null)
            throw new UserDoesNotHavePharmacyException();

        return pharmUser.Pharmacy.ID;
    }

    public async Task<ApplicationUserDTO[]> GetPharmacyUsersAsync(Guid pharmacyID)
    {
        var pharmUsers = await _context.PharmacyUsers
            .Where(pharmUser => pharmUser.Pharmacy.ID == pharmacyID)
            .AsNoTracking()
            .ToArrayAsync();

        var response = pharmUsers
            .Select(async pharmUser =>
                {
                    var user = await _userManager.FindByIdOrErrorAsync(pharmUser.ID.ToString());
                    var role = await _roleService.GetUserMainRoleAsync(user.Id);
                    return user.ToDTO(role);
                })
            .Select(t => t.Result)
            .ToArray();

        return response;
    }

    public async Task<ApplicationUserDTO> GetSinglePharmacyUserAsync(Guid id)
    {
        _ = await _context.PharmacyUsers.FindOrErrorAsync(id);

        var user = await _userManager.FindByIdAsync(id.ToString());

        var role = await _roleService.GetUserMainRoleAsync(user.Id);

        return user.ToDTO(role);
    }
}