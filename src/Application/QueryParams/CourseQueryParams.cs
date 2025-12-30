namespace CourseBookingAppBackend.src.Application.QueryParams;

public sealed class CourseQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
