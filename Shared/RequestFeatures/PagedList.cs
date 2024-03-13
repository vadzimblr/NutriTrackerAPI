namespace Shared.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(List<T> items,int totalCount, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = totalCount,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int PageNumber, int PageSize)
    {
        var totalCount = source.Count();
        var items = source.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
        return new PagedList<T>(items, totalCount, PageNumber, PageSize);
    }
}