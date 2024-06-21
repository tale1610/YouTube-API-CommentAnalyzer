using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
namespace YouTube_API_Comments_Analyzer;


public class Program
{
    public static async Task Main()
    {
        var webServer = new WebServer();

        var observer = new RequestObserver("Request Observer");

        // Koristimo TaskPoolScheduler za obradu zahteva na različitim nitima
        var requestStream = webServer.RequestStream.ObserveOn(TaskPoolScheduler.Default);

        var subscription = requestStream.Subscribe(observer);

        await webServer.StartAsync();

        Console.ReadLine();

        subscription.Dispose();
    }
}
