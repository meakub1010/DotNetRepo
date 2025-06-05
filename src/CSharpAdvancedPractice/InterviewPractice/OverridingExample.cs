// overriding - allows a subclass to provide a specific implementation of a method that is already defined in its superclass.


public class Animal
{
    public virtual void Speak() // this method must be marked as virtual or abstract or override in order to be overridden in derived classes
    {
        Console.WriteLine("Animal speaks");
    }
}
public class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks");
    }
}

public class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Cat meows");
    }
}