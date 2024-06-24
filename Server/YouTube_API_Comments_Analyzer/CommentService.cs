using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer
{
    public class CommentService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string apiKey = "AIzaSyA9u4UjD86t6RqntP2O7L7DOLkvv2t_xAI";

        public IEnumerable<Comment> FetchComments(string videoId)
        {
            var url = $"https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&videoId={videoId}&key={apiKey}";
            //var response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();

            var response = client.GetStringAsync(url).Result;

            //var jsonResponse = JObject.Parse(content);
            var jsonResponse = JObject.Parse(response);
            var commentsJson = jsonResponse["items"];

            List<Comment> comments = new List<Comment>();

            if (commentsJson != null)
            {
                foreach (var item in commentsJson)
                {
                    var snippet = item["snippet"]["topLevelComment"]["snippet"];
                    comments.Add(new Comment
                    {
                        AuthorDisplayName = snippet["authorDisplayName"].ToString(),
                        TextDisplay = snippet["textDisplay"].ToString(),
                        LikeCount = int.Parse(snippet["likeCount"].ToString()),
                        PublishedAt = DateTime.Parse(snippet["publishedAt"].ToString())
                    });
                }
            }

            return comments;
        }
    }
}
