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

    public RequestObserver(string name)
    {
        this.name = name;
    }

    public void OnNext(Comment comment)
    {
        Console.WriteLine($"{name}: Author: {comment.AuthorDisplayName}");
        Console.WriteLine($"Text: {comment.TextDisplay}");
        Console.WriteLine($"Likes: {comment.LikeCount}");
        Console.WriteLine($"Published At: {comment.PublishedAt}");
        Console.WriteLine("-------------------------------------------------");
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