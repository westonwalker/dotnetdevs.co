namespace dotnetdevs.ViewModels
{
    public class BlogPost
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Markdown { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
