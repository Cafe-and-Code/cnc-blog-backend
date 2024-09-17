namespace Blog.API.Models.Domain
{
    public class PostCategory
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public required Post post { get; set; }
        public required Category category { get; set; }
    }
}
