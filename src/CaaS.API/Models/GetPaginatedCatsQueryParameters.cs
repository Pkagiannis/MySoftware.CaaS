namespace CaaS.API.Models;

public sealed record GetPaginatedCatsQueryParameters(
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be a positive integer.")]
    int Page = 1,

    [Range(1, 100, ErrorMessage = "Page size must be a positive integer and cannot exceed 100.")]
    int PageSize = 10,

    string? Tag = null
);
