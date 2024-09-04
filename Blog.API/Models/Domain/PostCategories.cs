namespace Blog.API.Models.Domain
{
    public class PostCategories
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public required Posts post { get; set; }
        public required Categories category { get; set; }
    }
}
