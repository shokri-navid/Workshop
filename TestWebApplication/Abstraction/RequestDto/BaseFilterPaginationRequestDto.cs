namespace TestWebApplication.Abstraction.RequestDto;

public abstract class BaseFilterPaginationRequestDto : BasePaginationRequestDto
{
    /// <summary>
    /// Filter query that should be executed on data source 
    /// </summary>
    /// <example>Name.StartsWith(&amp;commat;0) &amp; Family.Contains(&amp;commat;1) and age > &amp;commat;2 </example>
    public string? FilterBody { get; set; }

    /// <summary>
    /// Args those are included inside Filter
    /// </summary>
    /// <example>n</example>
    public List<string>? Args { get; set; }
}