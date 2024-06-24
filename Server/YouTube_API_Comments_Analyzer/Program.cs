namespace YouTube_API_Comments_Analyzer;
public class Program
{
    public static void Main()
    {
        var webServer = new WebServer();
        webServer.Start();
        System.Console.ReadLine();
        webServer.Stop();
    }
}
