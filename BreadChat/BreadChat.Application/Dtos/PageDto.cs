namespace BreadChat.Application.Dtos;

public class PageDto<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalItems { get; private set; }
    public ICollection<T> Items { get; private set; }
    
    public PageDto(List<T> collection, int pageNumber, int pageSize, int total)
    {
        Items = collection;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = total;
        TotalPages = (total / pageSize) + 1;
    }
}