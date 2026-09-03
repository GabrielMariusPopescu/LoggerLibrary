namespace GenericLogger;

public static class ConsoleLoggerRegistrationService
{
    public static IServiceCollection RegisterLoggerService(this IServiceCollection services) 
        => services.AddSingleton<ILogger, ConsoleLogger>();
}