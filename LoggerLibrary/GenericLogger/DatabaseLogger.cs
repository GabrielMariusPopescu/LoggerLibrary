namespace GenericLogger;

public class DatabaseLogger(string connectionString) : ILogger
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
            return;

        await using var connection = new SqlConnection(connectionString);
        await using var command = connection.CreateCommand();

        command.CommandText = """
                              
                                          INSERT INTO ApplicationLogs (LogLevel, Message, ExceptionMessage, CreatedAt)
                                          VALUES (@level, @message, @exception, @createdAt)
                              """;

        command.Parameters.AddWithValue("@level", logLevel.ToString());
        command.Parameters.AddWithValue("@message", message);
        command.Parameters.AddWithValue("@exception", exception?.Message ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch
        {
            // Suppress logging exceptions to prevent crashing the main application
        }
    }
}