namespace TestWebApplication.Abstraction.Application;

public class Sorting
{
    public string? SortBy { get; set; }
    public bool IsAscending { get; set; }

    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(SortBy))
            return string.Empty;

        var sortMethod = IsAscending ? "" : "DESC";
        return $"{SortBy} {sortMethod}";
    }
}