namespace UniversityActivities.Application.Common.Models;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }

    public PagedResult(
        IReadOnlyList<T> items,
        int totalCount,
        int pageNumber,
        int pageSize,
        int totalPages)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
    }
}
