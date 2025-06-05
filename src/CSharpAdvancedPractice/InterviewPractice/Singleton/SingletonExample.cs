// when to use
/*
- logging service
- caching service
- configuration management
- database connection pool etc


GOOD:
- ensure single shared instance
- saves memory and resources
- good for shared configuration or state

BAD:
- global state make testing difficult
- can lead to tight coupling
- become a God Object if not managed properly, violates SRP
- can introduce hidden dependencies
- breaks DI principles


*/

public sealed class Singleton
{
    private static Singleton? _instance;
    private static readonly object _lock = new object();

    private Singleton()
    {
        // Private constructor to prevent instantiation from outside
    }

    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }

}

// Usage example
public class SingletonExample { 
    public static void Main(string[] args)
    {
        Singleton instance1 = Singleton.Instance;
        Singleton instance2 = Singleton.Instance;

        // Both instances should be the same
        Console.WriteLine(ReferenceEquals(instance1, instance2)); // Output: True
    }
}