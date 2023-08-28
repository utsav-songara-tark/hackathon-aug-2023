namespace Worker.Models
{
    public class Mems
    {
        public string postLink { get; set; }
        public string subreddit { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool nsfw { get; set; }
        public bool spoiler { get; set; }
        public string author { get; set; }
        public int ups { get; set; }
        public IList<string> preview { get; set; }

    }
}
