using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public class AsyncExample
{
    public async IAsyncEnumerable<int> GetNumbersAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000); // Simulate asynchronous work
            yield return i;
        }
    }
    public async Task RunAsync()
    {
        await foreach (var number in GetNumbersAsync())
        {
            Console.WriteLine($"Received number: {number}");
        }
    }
}