namespace ToDoList.Services;

public class ConsoleLoggerService : ILoggerService
{
    public void AddMessage(string message)
    {
        Console.WriteLine(message);
    }
}