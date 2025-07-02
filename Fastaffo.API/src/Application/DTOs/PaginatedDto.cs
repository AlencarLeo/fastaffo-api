namespace fastaffo_api.src.Application.DTOs;
public class PaginatedDto<T>
{
    public IEnumerable<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public PaginatedDto(IEnumerable<T> data, int count, int page, int pageSize)
    {
        Data = data;
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
