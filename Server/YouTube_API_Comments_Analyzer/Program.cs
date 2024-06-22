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

        // Pokretanje servera
        await webServer.StartAsync();

        Console.ReadLine();

        // Zaustavljanje servera
        webServer.Stop();
    }
}
