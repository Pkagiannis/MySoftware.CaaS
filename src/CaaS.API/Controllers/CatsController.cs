namespace CaaS.API.Controllers;

[ApiController]
[Route("api/cats")]
public class CatsController(
    ISender sender,
    IProblemDetailsFactory problemDetailsResult)
    : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IProblemDetailsFactory _problem = problemDetailsResult;

    [HttpPost("fetch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateCatsResponse>> CreateCats()
    {
        var result = await _sender.Send(new CreateCatsCommand());
        return result.Match<ActionResult<CreateCatsResponse>>(
            onSuccess: createdCatIds => Ok(createdCatIds),
            onFailure: error => error switch
            {
                { Code: "Cat.NoData" } => _problem.NotFoundResult(error),
                { Code: "Cat.NoNewCats" } => _problem.BadRequestResult(error),
                _ => _problem.InternalServerErrorResult(error)
            }
        );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCatByCatIdResponse>> GetCatByCatId(
        [FromRoute] string id)
    {
        var result = await _sender.Send(new GetCatByCatIdQuery(id));

        return result.Match<ActionResult<GetCatByCatIdResponse>>(
            onSuccess: cat => Ok(cat),
            onFailure: error => _problem.NotFoundResult(error)
            );
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPaginatedCatsResponse>> GetPaginatedCats(
        [FromQuery] GetPaginatedCatsQueryParameters queryParameters)
    {
        var result = await _sender.Send(new GetPaginatedCatsByTagQuery(
            queryParameters.Page,
            queryParameters.PageSize,
            queryParameters.Tag!));

        return result.Match<ActionResult<GetPaginatedCatsResponse>>(
            onSuccess: paginatedItems => Ok(paginatedItems),
            onFailure: error => _problem.NotFoundResult(error)
        );
    }
}
