namespace GenericLogger;

public static class FileLoggerRegistrationService
{
    public static IServiceCollection RegisterFileLoggerService(this IServiceCollection services) 
        => services.AddSingleton<ILogger>(new FileLogger("logs/app-log.txt"));
}