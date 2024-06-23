using java.io;
using opennlp.tools.namefind;
using opennlp.tools.util;
using OpenNLP.Tools.Tokenize;
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
using static opennlp.tools.formats.ad.ADSentenceStream;

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

    public void Start()
    {
        listener.Start();
        System.Console.WriteLine("Webserver pokrenut, pritisnite enter za prekid..");
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
                        ThreadPoolScheduler.Instance.Schedule(() =>
                        {
                            try
                            {
                                System.Console.WriteLine($"Handling request: {context.Request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");
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

    //    string videoId = request.QueryString["videoId"];

    //    if (!string.IsNullOrEmpty(videoId))
    //    {
    //        var commentService = new CommentService();
    //        var comments = await commentService.FetchCommentsAsync(videoId);

    //        // Slanje komentara ka klijentskoj strani koristeći RequestObserver
    //        System.Console.WriteLine($"Request: {request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");
    //        var observer = new RequestObserver($"Comment Observer{Thread.CurrentThread.ManagedThreadId}", response);
    //        string allComments = "";

    //        foreach (var comment in comments)
    //        {
    //            observer.OnNext(comment);
    //            allComments += comment.TextDisplay;
    //        }
    //        //System.Console.WriteLine(allComments);

    //        var tokenizer = new EnglishRuleBasedTokenizer(true);
    //        var tokens = tokenizer.Tokenize(allComments);

    //        NameFinderME model = null;
    //        InputStream modelFile = null;
    //        TokenNameFinderModel modelStream = null;
    //        //int i = 0;
    //        //foreach(var token in tokens)
    //        //{
    //        //    System.Console.WriteLine(i.ToString() + token);
    //        //    i++;
    //        //}

    //        try
    //        {
    //            modelFile = new FileInputStream("C:\\Users\\tasko\\Downloads\\ner\\en-ner-person.bin");
    //            modelStream = new TokenNameFinderModel(modelFile);
    //            model = new NameFinderME(modelStream);
    //            opennlp.tools.util.Span[] spans = model.find(tokens);

    //            System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}span length: " + spans.Length);

    //            foreach (Span span in spans)
    //            {
    //                System.Console.WriteLine(span.toString());
    //            }

    //        }
    //        catch (System.IO.FileNotFoundException e)
    //        {
    //            System.Console.WriteLine(e.StackTrace.ToString());
    //        }
    //        catch (System.IO.IOException e)
    //        {
    //            System.Console.WriteLine(e.StackTrace.ToString());
    //        }
    //        finally
    //        {
    //            try
    //            {
    //                modelFile.close();
    //            }
    //            catch (System.IO.IOException e)
    //            {
    //                System.Console.WriteLine(e.StackTrace.ToString());
    //            }
    //        }



    //    }
    //    else
    //    {
    //        string responseString = $"Ne postoji video za zadati videoId {videoId}";
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

            // Slanje komentara ka klijentskoj strani koristeći RequestObserver
            System.Console.WriteLine($"Request: {request.RawUrl}, Thread: {Thread.CurrentThread.ManagedThreadId}");
            var observer = new RequestObserver($"Comment Observer{Thread.CurrentThread.ManagedThreadId}", response);
            string allComments = "";

            foreach (var comment in comments)
            {
                observer.OnNext(comment);
                allComments += " ";
                allComments += comment.TextDisplay;
            }
            System.Console.WriteLine(allComments);

            var tokenizer = new EnglishRuleBasedTokenizer(true);
            var tokens = tokenizer.Tokenize(allComments);
            int i = 0;
            foreach (var token in tokens)
            {
                System.Console.WriteLine("Nit"+ Thread.CurrentThread.ManagedThreadId.ToString() + " indexReci["+ i.ToString() +"] " + token);
                i++;
            }

            try
            {
                using (var modelFile = new FileInputStream("C:\\Users\\tasko\\Downloads\\ner\\en-ner-person.bin"))
                {
                    var modelStream = new TokenNameFinderModel(modelFile);
                    var model = new NameFinderME(modelStream);
                    model.clearAdaptiveData();//OVO JE RESILO PROBLEM
                    var spans = model.find(tokens);

                    if (spans.Length > 0)
                    {
                        System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} span length: " + spans.Length);

                        foreach (Span span in spans)
                        {
                            System.Console.WriteLine($"Nit{Thread.CurrentThread.ManagedThreadId} " + span.ToString());
                        }
                    }
                    else
                    {
                        System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Na ovom videu nema imena u komentarima");
                    }
                    model.clearAdaptiveData();//OVO JE RESILO PROBLEM ne bas ipak ovde, kad sam ga popeo tamo gore to je resilo za kad su 3 zahteva, u sustini to kaze da model odma pocne sa fresh start a ne da mesa sta je naucio dodatno iz prethodnih interakcija
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
            catch (System.IO.IOException e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
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
                    System.Console.WriteLine($"Error while handling request: {ex.Message}");
                }
            },
            exception =>
            {
                System.Console.WriteLine($"Stream error: {exception.Message}");
            });
    }
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