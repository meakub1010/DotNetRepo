using System.Threading.Tasks;

class Program
{

    // in modern C#, the Main method can be async
    // but it is not recommended to use async Main in production code
    // because it can lead to unhandled exceptions and other issues
    // instead, use a separate async method and call it from Main
    static async Task Main(string[] args)
    {
        RecordDemo.Run();


        // run async example
        var asyncExample = new AsyncExample();
        asyncExample.RunAsync().GetAwaiter().GetResult(); // this is a blocking call to wait for the async method to complete


        // non blocking call
        await asyncExample.RunAsync(); // this will not block the main thread, but the program will exit before the async method completes if there is no delay or wait
    }
}