using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YouTube_API_Comments_Analyzer;
public class Request
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string VideoId { get; set; }
}
public class Comment
{
    public string AuthorDisplayName { get; set; }
    public string TextDisplay { get; set; }
    public int LikeCount { get; set; }
    public DateTime PublishedAt { get; set; }
}
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
                    Console.WriteLine($"{_name}:\n\tAutor: {comment.AuthorDisplayName}: " +
                                                $"\n\tTekst: {comment.TextDisplay}" +
                                                $"\n\t(Likes: {comment.LikeCount}, Published at: {comment.PublishedAt})");
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

    private IObservable<Comment> FetchAllCommentsAsync(string videoId)
    {
        return Observable.Create<Comment>(async observer =>
        {
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
                        var snippet = item["snippet"]["topLevelComment"]["snippet"];
                        var comment = new Comment
                        {
                            AuthorDisplayName = (string)snippet["authorDisplayName"],
                            TextDisplay = (string)snippet["textDisplay"],
                            LikeCount = (int)snippet["likeCount"],
                            PublishedAt = (DateTime)snippet["publishedAt"]
                        };
                        observer.OnNext(comment);

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

    private async Task<IEnumerable<Comment>> FetchRepliesAsync(string parentId)
    {
        var replies = new List<Comment>();
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
                    var snippet = item["snippet"];
                    var reply = new Comment
                    {
                        AuthorDisplayName = (string)snippet["authorDisplayName"],
                        TextDisplay = (string)snippet["textDisplay"],
                        LikeCount = (int)snippet["likeCount"],
                        PublishedAt = (DateTime)snippet["publishedAt"]
                    };
                    replies.Add(reply);
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