using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer;

public class Comment
{
    public string AuthorDisplayName { get; set; }
    public string TextDisplay { get; set; }
    public int LikeCount { get; set; }
    public DateTime PublishedAt { get; set; }
}
