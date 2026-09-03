namespace GenericLogger;

public class ConsoleLogger: ILogger
{
    public async Task LogTrace(string message)
        => await Log(LogLevel.Trace, message);

    public async Task LogDebug(string message) 
        => await Log(LogLevel.Debug, message);

    public async Task LogInformation(string message) 
        => await Log(LogLevel.Information, message);

    public async Task LogWarning(string message) 
        => await Log(LogLevel.Warning, message);

    public async Task LogError(string message, Exception? exception = null) 
        => await Log(LogLevel.Error, message, exception);

    public async Task LogCritical(string message, Exception? exception = null)
        => await Log(LogLevel.Critical, message, exception);

    public async Task Log(LogLevel logLevel, string message, Exception? exception = null)
    {
        if (logLevel == LogLevel.None)
            await Task.CompletedTask.ConfigureAwait(false);
        
        Console.WriteLine(exception != null
            ? $"{GetPrefix(logLevel)}: {message} - {exception.Message}"
            : $"{GetPrefix(logLevel)}: {message}");

        await Task.CompletedTask.ConfigureAwait(false);
    }

    private static string GetPrefix(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Information => "INFO",
            LogLevel.Error => "ERROR",
            _ => logLevel.ToString().ToUpper()
        };
}