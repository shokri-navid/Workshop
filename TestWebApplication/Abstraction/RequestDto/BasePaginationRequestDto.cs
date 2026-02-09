namespace TestWebApplication.Abstraction.RequestDto;

public abstract class BasePaginationRequestDto
{
    /// <summary>
    /// Size of result page (it should be positive number) 
    /// </summary>
    /// <example>10</example>
    public uint PageSize { get; set; } = 10;

    /// <summary>
    /// the number of pages those should be skipped (it should be positive number, starts from 0) 
    /// </summary>
    /// <example>0</example>
    public uint PageNumber { get; set; } = 0;

    /// <summary>
    /// the property name that list should be sort with
    /// </summary>
    /// <example>Version</example>
    public string? SortBy { get; set; }

    /// <summary>
    /// indicate that sorting should be ascending or descending
    /// </summary>
    /// <example>true</example>
    public bool IsAscending { get; set; } = false;
}