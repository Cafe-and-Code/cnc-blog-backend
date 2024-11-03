namespace Blog.API.Models.Domain
{
    public class PostCategory
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }

        public required Post Post { get; set; }
        public required Category Category { get; set; }
    }
}
