using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer;

internal class WebServer
{
    private readonly Subject<Request> requestSubject = new Subject<Request>();

    public IObservable<Request> RequestStream => requestSubject.AsObservable();

    public async Task StartAsync()
    {
        while (true)
        {
            // Simuliramo primanje zahteva
            Console.WriteLine("Unesite YouTube video ID:");
            var videoId = Console.ReadLine();

            var request = new Request
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                VideoId = videoId
            };

            // Logujemo primljen zahtev
            Console.WriteLine($"Primljen zahtev: {request.Id} za video {request.VideoId}");

            // Ubacujemo zahtev u stream
            requestSubject.OnNext(request);

            // Simulacija vremena obrade
            await Task.Delay(1000);
        }
    }
}

public class Request
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string VideoId { get; set; }
}