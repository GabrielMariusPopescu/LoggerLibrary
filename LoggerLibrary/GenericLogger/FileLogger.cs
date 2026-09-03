namespace GenericLogger;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private static readonly object Lock = new();

    public FileLogger(string filePath)
    {
        _filePath = filePath;

        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
    }

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

        var logEntry = exception != null
            ? $"{DateTime.UtcNow:O} [{GetPrefix(logLevel)}] {message} - {exception.Message}"
            : $"{DateTime.UtcNow:O} [{GetPrefix(logLevel)}] {message}";

        lock (Lock)
        {
            File.AppendAllText(_filePath, logEntry + Environment.NewLine);
        }
    }

    private static string GetPrefix(LogLevel level) 
        => level switch
    {
        LogLevel.Information => "INFO",
        LogLevel.Error => "ERROR",
        _ => level.ToString().ToUpper()
    };
}
