// method hiding - sub class hides a method of the base class with the same name and signature with the new keyword.

// compile time polymorphism

public class BaseClass
{
    public void Display()
    {
        Console.WriteLine("BaseClass Display");
    }
}

public class DerivedClass : BaseClass
{
    // Hiding the base class method using 'new' keyword
    public new void Display() // without 'new' keyword this will hide but compiler will give a warning
    {
        Console.WriteLine("DerivedClass Display");
    }
}

public class MethdodHidingExample
{
    public static void Main(string[] args)
    {
        BaseClass baseObj = new DerivedClass();
        baseObj.Display(); // Output: BaseClass Display
        DerivedClass derivedObj = new DerivedClass();
        derivedObj.Display(); // Output: DerivedClass Display
        // To call the base class method explicitly
        ((BaseClass)derivedObj).Display(); // Output: BaseClass Display
    }
}