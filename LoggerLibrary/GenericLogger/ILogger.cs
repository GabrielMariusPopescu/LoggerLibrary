namespace GenericLogger;

public interface ILogger
{
Task LogTrace(string message);

Task LogDebug(string message);

Task LogInformation(string message);

Task LogWarning(string message);

Task LogError(string message, Exception? exception = null);

Task LogCritical(string message, Exception? exception = null);

Task Log(LogLevel logLevel, string message, Exception? exception = null);
}
