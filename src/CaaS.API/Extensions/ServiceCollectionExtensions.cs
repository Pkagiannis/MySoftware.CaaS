namespace CaaS.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions.Add(
                    "traceId",
                    Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier
                );
            };
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IProblemDetailsFactory, ProblemDetailsFactory>();

        return services;
    }
}
