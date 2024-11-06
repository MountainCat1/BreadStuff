namespace BreadChat.Application.Dtos;

public class PageDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public ICollection<T> Items { get; set; }
    
    public PageDto(List<T> collection, int pageNumber, int pageSize, int total)
    {
        Items = collection;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = total;
        TotalPages = (total / pageSize) + 1;
    }
}