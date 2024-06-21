using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer;

public class RequestObserver : IObserver<Request>
{
    private readonly string _name;
    private static readonly HttpClient client = new HttpClient();
    private readonly string apiKey = "AIzaSyA9u4UjD86t6RqntP2O7L7DOLkvv2t_xAI";

    public RequestObserver(string name)
    {
        _name = name;
    }

    public void OnNext(Request request)
    {
        Console.WriteLine($"{_name}: Processing request {request.Id} for video {request.VideoId}");

        // Fetch comments
        FetchAllCommentsAsync(request.VideoId)
            .SubscribeOn(TaskPoolScheduler.Default)
            .Buffer(TimeSpan.FromSeconds(1)) // Collect comments for 1 second before processing
            .Subscribe(comments =>
            {
                foreach (var comment in comments)
                {
                    Console.WriteLine($"{_name}: {comment}");
                }
            });
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"{_name}: Error occurred: {error.Message}");
    }

    public void OnCompleted()
    {
        Console.WriteLine($"{_name}: All requests have been processed.");
    }

    private IObservable<string> FetchAllCommentsAsync(string videoId)
    {
        return Observable.Create<string>(async observer =>
        {
            var comments = new List<string>();
            var nextPageToken = string.Empty;

            try
            {
                do
                {
                    var url = $"https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&videoId={videoId}&key={apiKey}&pageToken={nextPageToken}";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JObject.Parse(content);
                    foreach (var item in jsonResponse["items"])
                    {
                        var commentText = (string)item["snippet"]["topLevelComment"]["snippet"]["textDisplay"];
                        observer.OnNext(commentText);

                        // Fetch replies to the comment
                        var commentId = (string)item["snippet"]["topLevelComment"]["id"];
                        var replies = await FetchRepliesAsync(commentId);
                        foreach (var reply in replies)
                        {
                            observer.OnNext(reply);
                        }
                    }

                    nextPageToken = (string)jsonResponse["nextPageToken"];
                } while (!string.IsNullOrEmpty(nextPageToken));

                observer.OnCompleted();
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
            }
        });
    }

    private async Task<IEnumerable<string>> FetchRepliesAsync(string parentId)
    {
        var replies = new List<string>();
        var nextPageToken = string.Empty;

        try
        {
            do
            {
                var url = $"https://www.googleapis.com/youtube/v3/comments?part=snippet&parentId={parentId}&key={apiKey}&pageToken={nextPageToken}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var jsonResponse = JObject.Parse(content);
                foreach (var item in jsonResponse["items"])
                {
                    var replyText = (string)item["snippet"]["textDisplay"];
                    replies.Add(replyText);
                }

                nextPageToken = (string)jsonResponse["nextPageToken"];
            } while (!string.IsNullOrEmpty(nextPageToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{_name}: Error fetching replies: {ex.Message}");
        }

        return replies;
    }
}

