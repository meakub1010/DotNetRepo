// subtypes must be substitutable for their base types without altering the correctness of the program

public interface IShape
{
    double Area();
}

public class Rectangle : IShape
{
    private double _width;
    private double _height;

    public Rectangle(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public double Area()
    {
        return _width * _height;
    }
}

public class Square : IShape
{
    private double _side;

    public Square(double side)
    {
        _side = side;
    }

    public double Area()
    {
        return _side * _side;
    }
}

public class Circle : IShape
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double Area()
    {
        return Math.PI * _radius * _radius;
    }
}

public class AreaCalculator
{
    public static void Main(string[] args)
    {
        IShape rectangle = new Rectangle(5, 10);
        IShape square = new Square(4);
        IShape circle = new Circle(3);

        AreaCalculator calculator = new AreaCalculator();
        Console.WriteLine($"Rectangle Area: {calculator.CalculateArea(rectangle)}");
        Console.WriteLine($"Square Area: {calculator.CalculateArea(square)}");
        Console.WriteLine($"Circle Area: {calculator.CalculateArea(circle)}");
    }
    public double CalculateArea(IShape shape)
    {
        return shape.Area();
    }
}