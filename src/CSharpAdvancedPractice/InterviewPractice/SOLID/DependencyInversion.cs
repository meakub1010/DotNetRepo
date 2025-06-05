// high level module should not depend on low level module, both should depend on abstractions

// coding against an interface that allows you to change the implementation without changing the code that uses it
// offers flexibility and decoupling for better maintainability and testability

public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class SmsService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}
public class Notification
{
    private readonly IMessageService _messageService;

// you can inject any message service Sms or Email
    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }
    public void Notify(string message)
    {
        _messageService.SendMessage(message);
    }
}