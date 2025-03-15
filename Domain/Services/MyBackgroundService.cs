using Microsoft.Extensions.Hosting;

namespace Domain.Services;

public class MyBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("MyBackgroundService is starting.");
        return Task.CompletedTask;
    }
}