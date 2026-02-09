namespace TestWebApplication.Abstraction.Application;

public abstract class AbstractQuery
{
    public Filter Filter { get; set; }
    public Sorting Sort
    {
        get
        {
            return new Sorting
            {
                IsAscending = _Sort.IsAscending,
                SortBy = MapProperty(_Sort.SortBy)
            };
        }
        set { _Sort = value; }
    }

    private Sorting _Sort;
    public Paging Paging { get; set; }
    public abstract Dictionary<string, string> FilterPropertyMapping();

    public string MapProperty(string property)
    {
        MappingDictionary =
            new Dictionary<string, string>(FilterPropertyMapping(), StringComparer.CurrentCultureIgnoreCase);
        if (!string.IsNullOrEmpty(property) && MappingDictionary.ContainsKey(property))
            return MappingDictionary[property];
        return property;
    }

    private Dictionary<string, string> MappingDictionary =
        new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
}