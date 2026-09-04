namespace GenericLogger;

public static class ConsoleLoggerRegistrationService
{
    public static IServiceCollection RegisterConsoleLoggerService(this IServiceCollection services) 
        => services.AddSingleton<ILogger, ConsoleLogger>();
}