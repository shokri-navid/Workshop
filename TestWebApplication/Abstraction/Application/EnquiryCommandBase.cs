namespace TestWebApplication.Abstraction.Application;

public abstract class EnquiryCommandBase : AbstractQuery
{
    protected virtual Dictionary<string, string> SortingPropertyMapping()
    {
        return FilterPropertyMapping();
    }

    public EnquiryCommandBase(string sortBy)
    {
        Paging = new Paging()
        {
            PageNumber = 0,
            PageSize = 10
        };
        Sort = new Sorting()
        {
            IsAscending = false,
            SortBy = MapProperty(sortBy),
        };
    }
}