using NReco.NLQuery.Matchers;
using NReco.NLQuery.Table;
using System;
using Xunit;

namespace NReco.NLQuery.Tests
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Klijent pokrenut...");

            try
            {
                // Primer video ID-jeva za testiranje
                string[] videoIds = { "sJKitE81lTw", "CCfTPU36AJE", "Z7V8S1O0ovc" };

                // Kreiranje niza Task objekata za svaki GET zahtev
                var tasks = new Task<string>[videoIds.Length];
                for (int i = 0; i < videoIds.Length; i++)
                {
                    tasks[i] = SendGetRequest(videoIds[i]);
                }

                // Čekanje završetka svih Task-ova
                await Task.WhenAll(tasks);

                // Ispis rezultata
                foreach (var task in tasks)
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        Console.WriteLine($"Response: {task.Result}");
                    }
                    else if (task.IsFaulted)
                    {
                        Console.WriteLine($"Request error: {task.Exception.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"General error: {e.Message}");
            }

            Console.WriteLine("Klijent završio sa slanjem zahteva.");
        }

        private static async Task<string> SendGetRequest(string videoId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/?videoId={videoId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                return $"Request error: {e.Message}";
            }
        }
    }
}

