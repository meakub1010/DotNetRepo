public interface ILoggerService
{
    public void Log(string message);
}
public sealed class LoggerService : ILoggerService // sealed class
{
    // read only private instance
    private static readonly LoggerService _instance = new LoggerService();

    // private constructor
    private LoggerService()
    {
        // initialize resource if needed
    }

    // public static member to access the instance
    public static LoggerService Instance => _instance;

    public void Log(string message)
    {
        Console.WriteLine($"[LOG - {DateTime.Now}] {message}");
    }
}