using Weather.Api.Database;
using Weather.Api.Repositories;

namespace Weather.Api;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMeteoRepository, MeteoRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(connectionString));

        services.AddSingleton<DbInitializer>();

        return services;
    }

}
