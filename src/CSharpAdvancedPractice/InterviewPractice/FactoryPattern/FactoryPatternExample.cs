public interface INotification
{
    void Send(string message);
}
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}
public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}

public class NotificationFactory
{
    public static INotification createNotification(string type)
    {
        return type switch
        {
            "Email" => new EmailNotification(),
            "SMS" => new SmsNotification(),
            _ => throw new ArgumentException("Invalid notification type")
        };
    }

    public static void Main(string[] args)
    {
        INotification emailNotification = NotificationFactory.createNotification("Email");
        emailNotification.Send("Hello via Email!");

        INotification smsNotification = NotificationFactory.createNotification("SMS");
        smsNotification.Send("Hello via SMS!");
    }   
}