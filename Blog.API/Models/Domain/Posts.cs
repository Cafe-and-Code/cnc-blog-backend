namespace Blog.API.Models.Domain
{
    public class Posts
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryIds { get; set; }
        public Guid AuthorId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Categories Category { get; set; }
        public Users Author { get; set; }
    }
}
