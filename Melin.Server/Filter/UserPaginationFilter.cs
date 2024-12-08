namespace Melin.Server.Filter;

public class UserPaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public UserPaginationFilter()
    {
        this.PageNumber = 1;
        this.PageSize = 25;
    }

    public UserPaginationFilter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize < 25 ? 25 : pageSize;
    }
}