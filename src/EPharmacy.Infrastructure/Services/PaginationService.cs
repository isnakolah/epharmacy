using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Infrastructure.Services;

internal sealed class PaginationService : IPaginate
{
    private readonly IConfigurationProvider _mapperConfig;
    private readonly IUriService _uriService;
    private readonly IDateTime _dateTime;

    public PaginationService(IConfigurationProvider mapperConfig, IUriService uriService, IDateTime dateTime)
    {
        (_mapperConfig, _uriService, _dateTime) = (mapperConfig, uriService, dateTime);
    }

    public async Task<PaginatedServiceResult<Out>> CreateAsync<In, Out>(
        IQueryable<In> source,
        PaginationFilter filter,
        CancellationToken cancellationToken,
        List<QueryParam>? queryParams = null)
        where In : AuditableEntity where Out : class
    {
        var dateOnly = filter.DateOnly;

        var pageNumber = filter.PageNumber < 1
            ? 1
            : filter.PageNumber;

        var pageSize = filter.PageSize < 1
            ? 10
            : filter.PageSize > 50
            ? 50
            : filter.PageSize;

        var from = filter.From is null
            ? _dateTime.Now.AddDays(-7)
            : filter.From;

        var to = filter.To is null
            ? _dateTime.Now
            : filter.To;

        source = dateOnly
            ? source.Where(x => x.CreatedOn >= from && x.CreatedOn <= to)
            : source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var data = await source
            .AsNoTracking()
            .ProjectTo<Out>(_mapperConfig)
            .ToArrayAsync(cancellationToken);

        var totalRecords = dateOnly
            ? data.Length
            : await source.CountAsync();

        pageSize = dateOnly
            ? totalRecords
            : pageSize;

        var totalPages = !totalRecords.Equals(0)
            ? Convert.ToInt32(Math.Ceiling((double)totalRecords / pageSize))
            : 0;

        var paginationFilter = new PaginationFilter(pageSize, from, to, dateOnly);

        var nextPage = pageNumber >= 1 && pageNumber < totalPages
            ? _uriService.GetPageUri(paginationFilter.ChangePageNumber(pageNumber + 1), queryParams)
            : null;

        var previousPage = pageNumber - 1 >= 1 && pageNumber <= totalPages
            ? _uriService.GetPageUri(paginationFilter.ChangePageNumber(pageNumber - 1), queryParams)
            : null;

        var firstPage = totalPages > 0
            ? _uriService.GetPageUri(paginationFilter.ChangePageNumber(1), queryParams)
            : null;

        var lastPage = totalPages > 0
            ? _uriService.GetPageUri(paginationFilter.ChangePageNumber(totalPages), queryParams)
            : null;

        var response = new PaginatedServiceResult<Out>(
            pageNumber, totalPages, pageSize, totalRecords, firstPage, lastPage, nextPage, previousPage, from, to, data);

        return response;
    }
}