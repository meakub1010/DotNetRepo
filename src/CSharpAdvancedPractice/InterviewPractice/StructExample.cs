using System.Diagnostics.Contracts;

public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Example usage of the Point struct
public class StructExample
{
    public static void Main(string[] args)
    {
        Point p1 = new Point(10, 20);
        Point p2 = p1; // Copying the struct creates a new instance
        p2.X = 30; // Modifying p2 does not affect p1   
    }
}