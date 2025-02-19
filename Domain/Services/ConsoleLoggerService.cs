namespace Domain.Services;

public class ConsoleLoggerService : ILoggerService
{
    public void AddMessage(string message)
    {
        Console.WriteLine(message);
    }
}