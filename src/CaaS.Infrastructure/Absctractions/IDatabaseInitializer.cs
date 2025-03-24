namespace CaaS.Infrastructure.Absctractions;
public interface IDatabaseInitializer
{
    Task MigrateDbAsync();
}