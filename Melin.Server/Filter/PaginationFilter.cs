using ReferenceType = Melin.Server.Models.ReferenceType;

namespace Melin.Server.Filter;

public class PaginationFilter
{
    public ReferenceType? ReferenceType { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter()
    {
        this.PageNumber = 1;
        this.PageSize = 10;
    }
    public PaginationFilter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize < 10 ? 10 : pageSize;
    }
}