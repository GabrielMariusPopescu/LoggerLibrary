namespace GenericLogger;

public static class FileLoggerRegistrationService
{
    public static IServiceCollection RegisterLoggerService(this IServiceCollection services) 
        => services.AddSingleton<ILogger>(new FileLogger("logs/app-log.txt"));
}