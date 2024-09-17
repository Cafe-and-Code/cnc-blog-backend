using Blog.API.Models.Domain;

namespace Blog.API.Models.DTO
{
    public class PostCategoryDTO
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public required Post post { get; set; }
        public required Category category { get; set; }
    }
}
