using EPharmacy.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EPharmacy.Application.Common.Extensions;

public static class IQueryableExtensions
{
    /// <summary>
    /// Gets the first item of an enumerable or throws NotFoundException
    /// </summary>
    /// <typeparam name="T">Type of the IQueryable</typeparam>
    /// <param name="source">The queryable object</param>
    /// <param name="ID">ID of the entity to be retrieved</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Task of type T</returns>
    /// <exception cref="NotFoundException"></exception>
    public static async Task<T> FirstOrErrorAsync<T>(this IQueryable<T> source, Guid ID, CancellationToken cancellationToken = new()) where T : class
    {
        var entity = await source.FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            throw new NotFoundException(typeof(T).Name, ID);

        return entity;
    }

    /// <summary>
    /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a throws a not found exception if no such element is found.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An System.Linq.IQueryable`1 to return the first element of.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="ID"></param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains default
    ///   ( TSource ) if source is empty or if no element passes the test specified by
    ///   predicate ; otherwise, the first element in source that passes the test specified
    ///     by predicate.
    /// </returns>
    /// <exception cref="NotFoundException">No such element exists</exception>
    public static async Task<T> FirstOrErrorAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, object ID, CancellationToken cancellationToken = default)
    {
        var entity = await source.FirstOrDefaultAsync(predicate, cancellationToken);

        if (entity is null)
            throw new NotFoundException(typeof(T).Name, ID);

        return entity;
    }

    /// <summary>
    /// Gets the first item of an enumerable or throws NotFoundException
    /// </summary>
    /// <typeparam name="T">Type of the IQueryable</typeparam>
    /// <param name="source">The queryable object</param>
    /// <param name="ID">ID of the entity to be retrieved</param>
    /// <returns>Entity of type T</returns>
    /// <exception cref="NotFoundException"></excepti
    public static T FirstOrError<T>(this IQueryable<T> source, Guid ID) where T : class
    {
        var entity = source.FirstOrDefault();

        if (entity is null)
            throw new NotFoundException(typeof(T).Name, ID);

        return entity;
    }
}
