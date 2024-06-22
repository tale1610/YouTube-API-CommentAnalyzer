using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer;

public class WebServer
{
    private readonly HttpListener listener = new HttpListener();
    private IDisposable observableSubscription;

    public IObservable<HttpListenerContext> RequestStream { get; private set; }

    public WebServer()
    {
        listener.Prefixes.Add("http://localhost:8080/");
    }

    public async Task StartAsync()
    {
        listener.Start();
        Console.WriteLine("Webserver is running... Press enter to exit.");
        Run();
    }

    public void Stop()
    {
        listener.Stop();
    }

    public IObservable<HttpListenerContext> GetContextAsync()
    {
        return Observable.Create<HttpListenerContext>(observer =>
        {
            Task.Run(async () =>
            {
                while (listener.IsListening)
                {
                    try
                    {
                        var context = await listener.GetContextAsync();
                        //var comment = ConvertToComment(context);
                        ThreadPoolScheduler.Instance.Schedule(() =>
                        {
                            try
                            {
                                Console.WriteLine($"Handling request: {context.Request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");
                                observer.OnNext(context);
                            }
                            catch (Exception ex)
                            {
                                observer.OnError(ex);
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }
                }
            });

            return Disposable.Empty;
        });
    }

    //public async Task HandleRequest(HttpListenerContext context)
    //{
    //    HttpListenerRequest request = context.Request;
    //    HttpListenerResponse response = context.Response;

    //    Console.WriteLine($"Request: {request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");

    //    string videoId = request.QueryString["videoId"];
    //    if (!string.IsNullOrEmpty(videoId))
    //    {
    //        var commentService = new CommentService();
    //        var comments = await commentService.FetchCommentsAsync(videoId);

    //        var sb = new StringBuilder();
    //        foreach (var comment in comments)
    //        {
    //            sb.AppendLine($"Author: {comment.AuthorDisplayName}, Comment: {comment.TextDisplay}, Likes: {comment.LikeCount}, Published At: {comment.PublishedAt}");
    //        }

    //        string responseString = sb.ToString();
    //        byte[] buf = Encoding.UTF8.GetBytes(responseString);
    //        response.ContentLength64 = buf.Length;
    //        response.OutputStream.Write(buf, 0, buf.Length);
    //    }
    //    else
    //    {
    //        string responseString = "Invalid video ID";
    //        byte[] buf = Encoding.UTF8.GetBytes(responseString);
    //        response.ContentLength64 = buf.Length;
    //        response.OutputStream.Write(buf, 0, buf.Length);
    //    }

    //    context.Response.OutputStream.Close();
    //}

    public async Task HandleRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string videoId = request.QueryString["videoId"];
        if (!string.IsNullOrEmpty(videoId))
        {
            var commentService = new CommentService();
            var comments = await commentService.FetchCommentsAsync(videoId);

            // Pretvori HTTP zahtev u Comment objekte
            List<Comment> commentList = ConvertToComments(comments);

            // Slanje komentara ka klijentskoj strani koristeći RequestObserver
            var observer = new RequestObserver("Comment Observer");
            foreach (var comment in commentList)
            {
                observer.OnNext(comment);
            }

            string responseString = "Comments sent to client";
            byte[] buf = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buf.Length;
            response.OutputStream.Write(buf, 0, buf.Length);
        }
        else
        {
            string responseString = "Invalid video ID";
            byte[] buf = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buf.Length;
            response.OutputStream.Write(buf, 0, buf.Length);
        }

        context.Response.OutputStream.Close();
    }

    private List<Comment> ConvertToComments(IEnumerable<Comment> comments)
    {
        // Ovde se samo vraća lista komentara bez promene
        return comments.ToList();
    }


    public void Run()
    {
        IObservable<HttpListenerContext> obs = this.GetContextAsync();

        this.observableSubscription = obs.Subscribe(
            async context =>
            {
                try
                {
                    await HandleRequest(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while handling request: {ex.Message}");
                }
            },
            exception =>
            {
                Console.WriteLine($"Stream error: {exception.Message}");
            });
    }
}

//public class Request
//{
//    public Guid Id { get; set; }
//    public DateTime Timestamp { get; set; }
//    public string VideoId { get; set; }
//}