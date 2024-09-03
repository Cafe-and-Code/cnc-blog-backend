namespace Blog.API.Models.Domain
{
    public class PostCategories
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public Posts post { get; set; }
        public Categories category { get; set; }
    }
}
