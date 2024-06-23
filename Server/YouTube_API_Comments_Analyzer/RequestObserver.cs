using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer;

public class RequestObserver : IObserver<Comment>
{
    private readonly string name;
    private readonly HttpListenerResponse response;

    public RequestObserver(string name, HttpListenerResponse response)
    {
        this.name = name;
        this.response = response;
    }

    public void OnNext(Comment comment)
    {
        string data = $"{name}: Author: {comment.AuthorDisplayName}\n" +
                      $"Text: {comment.TextDisplay}\n" +
                      $"Likes: {comment.LikeCount}\n" +
                      $"Published At: {comment.PublishedAt}\n" +
                      "-------------------------------------------------\n";
        // Konvertuj string u niz bajtova
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        // Piši u OutputStream
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Flush();
        // Ovde se može dodati provera da li je konekcija zatvorena
        if (!response.OutputStream.CanWrite)
        {
            Console.WriteLine($"{name}: OutputStream closed.");
        }
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"{name}: Error occurred: {error.Message}");
    }

    public void OnCompleted()
    {
        Console.WriteLine($"{name}: All comments processed.");
    }
}

//public class RequestObserver : IObserver<HttpListenerContext>
//{
//    private readonly string name;

//    public RequestObserver(string name)
//    {
//        this.name = name;
//    }

//    public void OnNext(HttpListenerContext context)
//    {
//        var request = context.Request;
//        var response = context.Response;

//        Console.WriteLine($"Request: {request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");

//        byte[] buf = System.Text.Encoding.UTF8.GetBytes("Returning response");
//        response.ContentLength64 = buf.Length;
//        response.OutputStream.Write(buf, 0, buf.Length);

//        context.Response.OutputStream.Close();
//    }

//    public void OnError(Exception ex)
//    {
//        Console.WriteLine($"{name}: Error occurred: {ex.Message}");
//    }

//    public void OnCompleted()
//    {
//        Console.WriteLine($"{name}: All requests processed.");
//    }
//}