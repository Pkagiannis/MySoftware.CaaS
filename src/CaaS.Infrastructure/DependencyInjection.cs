namespace CaaS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddPersistence(config);
        services.AddCatsServiceHttpClient(config);

        return services;
    }
}
