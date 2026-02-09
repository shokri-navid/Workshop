namespace TestWebApplication.Abstraction.Application;

public class Paging
{
    public int PageSize { get; set; }


    public int PageNumber { get; set; }

    public int Skip => Convert.ToInt32(PageNumber * PageSize);
}