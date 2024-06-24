using System.Net;
using System.Text;

namespace YouTube_API_Comments_Analyzer;

public class RequestObserver : IObserver<string>
{
    private readonly string name;
    private readonly HttpListenerResponse response;

    public RequestObserver(string name, HttpListenerResponse response)
    {
        this.name = name;
        this.response = response;
    }

    //public void OnNext(Comment comment)
    //{
    //    string data = $"{name}:\nAuthor: {comment.AuthorDisplayName}\n" +
    //                  $"Text: {comment.TextDisplay}\n" +
    //                  $"Likes: {comment.LikeCount}\n" +
    //                  $"Published At: {comment.PublishedAt}\n" +
    //                  "-------------------------------------------------\n";
    //    // Konvertuj string u niz bajtova
    //    byte[] buffer = Encoding.UTF8.GetBytes(data);
    //    // Piši u OutputStream
    //    response.OutputStream.Write(buffer, 0, buffer.Length);
    //    response.OutputStream.Flush();
    //    // Ovde se može dodati provera da li je konekcija zatvorena
    //    if (!response.OutputStream.CanWrite)
    //    {
    //        Console.WriteLine($"{name}: OutputStream closed.");
    //    }
    //}
    
    public void OnNext(string data)
    {
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