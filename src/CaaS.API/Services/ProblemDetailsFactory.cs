namespace CaaS.API.Services;

public class ProblemDetailsFactory(IHttpContextAccessor httpContextAccessor) : IProblemDetailsFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public ObjectResult BadRequestResult(Error error)
    {
        var context = _httpContextAccessor.HttpContext;
        var problem = CreateProblemDetails(
            context,
            StatusCodes.Status400BadRequest,
            error.Code,
            error.Message
        );
        return new BadRequestObjectResult(problem);
    }

    public ObjectResult NotFoundResult(Error error)
    {
        var context = _httpContextAccessor.HttpContext;
        var problem = CreateProblemDetails(
            context,
            StatusCodes.Status404NotFound,
            error.Code,
            error.Message
        );
        return new NotFoundObjectResult(problem);
    }

    public ObjectResult InternalServerErrorResult(Error error)
    {
        var context = _httpContextAccessor.HttpContext;
        var problem = CreateProblemDetails(
            context,
            StatusCodes.Status500InternalServerError,
            error.Code,
            error.Message
        );
        return new ObjectResult(problem) { StatusCode = StatusCodes.Status500InternalServerError };
    }

    private ProblemDetails CreateProblemDetails(
        HttpContext? context,
        int statusCode,
        string code,
        string detail)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = code,
            Detail = detail,
            Extensions =
            {
                ["traceId"] = Activity.Current?.Id ?? context?.TraceIdentifier
            }
        };
    }
}
