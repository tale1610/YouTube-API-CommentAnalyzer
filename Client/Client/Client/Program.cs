using NReco.NLQuery.Matchers;
using NReco.NLQuery.Table;
using System;
using Xunit;

namespace NReco.NLQuery.Tests
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                // Kreiranje liste autora za pretragu
                List<string> videos = new List<string> { "sJKitE81lTw", "CCfTPU36AJE", "Z7V8S1O0ovc" }; //5,2,24 valjda

                // Kreiranje liste taskova za svakog autora
                List<Task> tasks = new List<Task>();
                foreach (var video in videos)
                {
                    tasks.Add(ProcessVideoAsync(video));
                }

                // Čekanje da se svi taskovi završe
                await Task.WhenAll(tasks);

                Console.WriteLine("Odgovoreno na sve zahteve.");
            }
        }

        static async Task ProcessVideoAsync(string video)
        {
            string serverUrl = $"http://localhost:8080/?author={video}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(serverUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Odgovor za video: '{video}':");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Greska na serveru za video: '{video}': {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"HTTP request greska za video: '{video}': {e.Message}");
                }
            }
        }
    }
}

