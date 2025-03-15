using Microsoft.Extensions.Hosting;

namespace Domain.Services;

public class HostedService : IHostedService, IDisposable
{
    public Task StartAsync(CancellationToken token)
    {
        Console.WriteLine("Hosted service is started.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken token)
    {
        Console.WriteLine("Hosted service is stopped.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}