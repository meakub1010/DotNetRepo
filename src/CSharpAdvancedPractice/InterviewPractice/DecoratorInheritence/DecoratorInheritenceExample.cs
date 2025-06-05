// Inheritence 

public class Coffee
{
    public virtual string GetDescription()
    {
        return "Coffee";
    }

    public virtual double GetCost()
    {
        return 2.0; // Base cost of coffee
    }
}
public class MilkCoffee : Coffee
{
    public override string GetDescription()
    {
        return base.GetDescription() + ", Milk";
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.5; // Additional cost for milk
    }
}

// and so on for other add-ons like Sugar, Whipped Cream, etc.
public class SugarCoffee : MilkCoffee
{
    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar";
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.2; // Additional cost for sugar
    }
}

// inheritence happens compile time, we need to implement subclass for every combination
// this is not flexible, we need to use composition instead of inheritence

// DECORATOR PATTERN
// Decorator pattern allows us to add new functionality to an existing object without altering its structure
// It is achieved by creating a set of decorator classes that are used to wrap concrete components

public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

public class PlainCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Plain Coffee";
    }

    public double GetCost()
    {
        return 2.0; // Base cost of coffee
    }
}

public class MilkDecorator : ICoffee
{
    private readonly ICoffee _coffee;

    public MilkDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public string GetDescription()
    {
        return _coffee.GetDescription() + ", Milk";
    }

    public double GetCost()
    {
        return _coffee.GetCost() + 0.5; // Additional cost for milk
    }
}

public class SugarDecorator : ICoffee
{
    private readonly ICoffee _coffee;

    public SugarDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public string GetDescription()
    {
        return _coffee.GetDescription() + ", Sugar";
    }

    public double GetCost()
    {
        return _coffee.GetCost() + 0.2; // Additional cost for sugar
    }    
}

// Usage example
public class DecoratorInheritenceExample
{
    public static void Main(string[] args)
    {
        ICoffee coffee = new PlainCoffee();
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");
        coffee = new MilkDecorator(coffee);
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");
        coffee = new SugarDecorator(coffee);
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");
        // Output:
        // Plain Coffee - $2.0
        // Plain Coffee, Milk - $2.5  
        // Plain Coffee, Milk, Sugar - $2.7
        // This allows us to dynamically add functionality without modifying the original object  
    }
}


// When to use Decorator Pattern or Inheritance?
// Use Decorator Pattern when:
/*

- single extension is needed, use inheritance
- multiple extensions are needed, use decorator pattern
- mutiple combinations, runtime flexibility is needed, use decorator pattern
- follow open/closed principle, decorator pattern is better
*/