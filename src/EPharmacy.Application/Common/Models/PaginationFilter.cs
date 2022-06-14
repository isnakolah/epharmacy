namespace EPharmacy.Application.Common.Models;

/// <summary>
/// PaginationFilter class 
/// </summary>
public class PaginationFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; init; } = 10;
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public bool DateOnly { get; init; } = false;

    public PaginationFilter()
    {
    }

    public PaginationFilter(int pageNumber, int pageSize, DateTime? from, DateTime? to, bool dateOnly)
    {
        (PageNumber, PageSize, From, To, DateOnly) = (pageNumber, pageSize, from, to, dateOnly);
    }

    public PaginationFilter(int pageSize, DateTime? from, DateTime? to, bool dateOnly)
    {
        (PageSize, From, To, DateOnly) = (pageSize, from, to, dateOnly);
    }

    public PaginationFilter ChangePageNumber(int pageNumber)
    {
        PageNumber = pageNumber;
        return this;
    }
}
