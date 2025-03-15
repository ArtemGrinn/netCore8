using Microsoft.Extensions.Hosting;

namespace Domain.Services;

public class HangfireJob
{
    public void Execute()
    {
        Console.WriteLine("MyBackgroundService is starting.");
    }
}