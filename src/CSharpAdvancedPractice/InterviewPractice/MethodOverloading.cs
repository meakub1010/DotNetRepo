// overloading - having multiple methods with the samne name but different parameters

public class MethodOverloading
{
    // Overloaded methods with different parameters

    Calculator calculator = new Calculator();
    public static void Main(string[] args)
    {
        MethodOverloading methodOverloading = new MethodOverloading();
        methodOverloading.Run();
    }
    public void Run()
    {
        // Example usage of overloaded methods
        Console.WriteLine(calculator.Add(5, 10)); // Calls Add(int, int)
        Console.WriteLine(calculator.Add(5, 10, 15)); // Calls Add(int, int, int)
        Console.WriteLine(calculator.Add(5.5, 10.5)); // Calls Add(double, double)
    }

}

public class Calculator
{
    // Method to add two integers
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Overloaded method to add three integers
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    // Overloaded method to add two doubles
    public double Add(double a, double b)
    {
        return a + b;
    }
}