interface ILogger
{
    void Log(string message);
}
abstract class BaseLogger : ILogger
{
    public abstract void Log(string message);

    // This is a concrete method that can be used by derived classes
    protected void WriteToConsole(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}
class ConsoleLogger : BaseLogger
{
    public override void Log(string message)
    {
        // Call the base class method to write to console
        WriteToConsole(message);
    }
}
class FileLogger : BaseLogger
{
    public override void Log(string message)
    {
        // Simulate writing to a file (for demonstration purposes)
        Console.WriteLine($"Writing to file: {message}");
    }
}