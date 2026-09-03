namespace GenericLogger;

public static class DatabaseLoggerRegistrationService
{
    public static IServiceCollection RegisterDatabaseLoggerService(this IServiceCollection services,
        string connectionString) => services.AddSingleton<ILogger>(new DatabaseLogger(connectionString));
}