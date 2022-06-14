using EPharmacy.Application.Common.Constants;
using EPharmacy.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Infrastructure.Services;

internal sealed class CurrentUserPharmacy : ICurrentUserPharmacy
{
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly IPharmacyUserService _pharmacyUserService;

    public CurrentUserPharmacy(IApplicationDbContext context, ICurrentUserService currentUser, IMemoryCache memoryCache, IPharmacyUserService pharmacyUserService)
    {
        (_context, _currentUser, _memoryCache, _pharmacyUserService) = (context, currentUser, memoryCache, pharmacyUserService); 
    }

    public async Task<Guid> GetIDAsync()
    {
        return await GetPharmacyID(_currentUser.UserId);
    }

    private async Task<Guid> GetPharmacyID(string userID)
    {
        var userPharmacyKey = _currentUser.UserId + CacheKeys.PHARMACY_ID;

        if (_memoryCache.TryGetValue(userPharmacyKey, out Guid cachedPharmacyID))
            return cachedPharmacyID;

        var pharmacyID = await _pharmacyUserService.GetUserPharmacyIDAsync(userID);

        if (await PharmacyExists(pharmacyID))
            return _memoryCache.Set(userPharmacyKey, pharmacyID);

        throw new NotFoundException(nameof(_context.Pharmacies), pharmacyID);
    }

    private async Task<bool> PharmacyExists(Guid pharmacyID)
    {
        return await _context.Pharmacies.AnyAsync(pharm => pharm.ID == pharmacyID);
    }
}