using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace YouTube_API_Comments_Analyzer;

public class WebServer
{
    private readonly HttpListener listener = new HttpListener();
    private IDisposable observableSubscription;

    public WebServer()
    {
        listener.Prefixes.Add("http://localhost:8080/");
    }

    public void Start()
    { 
        listener.Start();
        System.Console.WriteLine("Webserver pokrenut, pritisnite enter za prekid..");
        IObservable<HttpListenerContext> obs = Observable.Create<HttpListenerContext>(async (observer) => 
        {
            while (listener.IsListening)
            {
                System.Console.WriteLine($"Nit koja stalno osluskuje: {Thread.CurrentThread.ManagedThreadId}");
                try
                {
                    var context = await listener.GetContextAsync();
                    TaskPoolScheduler.Default.Schedule(() =>
                    { 
                        try
                        {
                            System.Console.WriteLine($"Primljen zahtev: {context.Request.RawUrl}, Nit: {Thread.CurrentThread.ManagedThreadId}");
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
            return Disposable.Empty;
        });
        this.observableSubscription = obs.SubscribeOn(CurrentThreadScheduler.Instance).Subscribe(
            context =>
            {
                try
                {
                    HandleRequest(context);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Error while handling request: {ex.Message}");
                }
            },
            exception =>
            {
                System.Console.WriteLine($"Stream error: {exception.Message}");
            });
    }


    public void HandleRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string videoId = request.QueryString["videoId"];

        if (!string.IsNullOrEmpty(videoId)) 
        {
            var commentService = new CommentService();
            var comments = commentService.FetchComments(videoId);

            // Slanje komentara ka klijentskoj strani koristeći RequestObserver
            System.Console.WriteLine($"Zahtev: {request.RawUrl}, Obradjuje nit: {Thread.CurrentThread.ManagedThreadId}");
            var observer = new RequestObserver($"Comment Observer{Thread.CurrentThread.ManagedThreadId}", response);
            string allComments = "";
            string data = "";

            foreach (var comment in comments)
            {
                data += $"Comment Observer{Thread.CurrentThread.ManagedThreadId}:\n" +
                      $"Author: {comment.AuthorDisplayName}\n" +
                      $"Text: {comment.TextDisplay}\n" +
                      $"Likes: {comment.LikeCount}\n" +
                      $"Published At: {comment.PublishedAt}\n" +
                      "-------------------------------------------------\n";
                //observer.OnNext(comment);
                allComments += " ";
                allComments += comment.TextDisplay;
            }

            string analyzedResults = NamedEntityRecognition.Analyze(allComments);
            data += analyzedResults;
            observer.OnNext(data);
            observer.OnCompleted();
        }
        else
        {
            string responseString = $"Ne postoji video za zadati videoId {videoId}";
            byte[] buf = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buf.Length;
            response.OutputStream.Write(buf, 0, buf.Length);
        }

        context.Response.OutputStream.Close();
    }
    public void Stop()
    {
        observableSubscription.Dispose();
        listener.Stop();
    }


}