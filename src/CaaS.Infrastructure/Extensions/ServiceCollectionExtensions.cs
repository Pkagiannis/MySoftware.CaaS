namespace CaaS.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICatRepository, CatRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddDbContext<CaasDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("CaaSDbConnection")));

        services.AddDbInitializer();

        return services;
    }

    public static IServiceCollection AddCatsServiceHttpClient(this IServiceCollection services, IConfiguration config)
    {
        var optionsSection = config.GetSection(CaasApiOptions.SectionName);
        var options = optionsSection.Get<CaasApiOptions>();

        services.AddOptions<CaasApiOptions>()
            .Bind(optionsSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient(
            CreateCatsHandler.HttpClientName,
            client =>
            {
                client.BaseAddress = options?.Uri;
                client.DefaultRequestHeaders.Add("x-api-key", options?.ApiKey);
            });

        return services;
    }

    public static IServiceCollection AddDbInitializer(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseInitializer>(provider =>
        {
            var dbContext = provider.GetRequiredService<CaasDbContext>();
            var logger = provider.GetRequiredService<ILogger<DatabaseInitializer>>();

            return new DatabaseInitializer(dbContext, logger);
        });

        return services;
    }
}
