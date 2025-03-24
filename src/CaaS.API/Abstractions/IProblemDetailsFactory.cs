namespace CaaS.API.Abstractions;

public interface IProblemDetailsFactory
{
    ObjectResult BadRequestResult(Error error);
    ObjectResult InternalServerErrorResult(Error error);
    ObjectResult NotFoundResult(Error error);
}