public class PagedRequestDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}


public class PagedResultDto<T>
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();
}
