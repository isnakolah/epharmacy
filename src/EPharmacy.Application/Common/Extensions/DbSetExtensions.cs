using EPharmacy.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Common.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> FindOrErrorAsync<T>(this DbSet<T> entitySet, Guid ID) where T : class
    {
        var entity = await entitySet.FindAsync(ID);

        if (entity is null)
            throw new NotFoundException(nameof(entitySet), ID);

        return entity;
    }

    public static T FindOrError<T>(this DbSet<T> entitySet, Guid ID) where T : class
    {
        var entity = entitySet.Find(ID);

        if (entity is null)
            throw new NotFoundException(nameof(entitySet), ID);

        return entity;
    }
}
