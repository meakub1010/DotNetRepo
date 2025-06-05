// no client should be forced to depend on methods/interfaces it does not use

public interface IPrinter
{
    void Print();
}
public interface IScanner
{
    void Scan();
}

public class SmartPrinter : IPrinter, IScanner
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }

    public void Scan()
    {
        Console.WriteLine("Scanning document...");
    }
}

public class SimplePrinter : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }
}

// avoid things like below
public interface IMachine
{
    void Print();
    void Scan();
}