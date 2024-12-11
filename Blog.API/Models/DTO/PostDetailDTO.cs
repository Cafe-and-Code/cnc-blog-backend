namespace Blog.API.Models.DTO
{
    public class PostDetailDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string TitleImageUrl { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
        public List<string>? Categories { get; set; }
        public required string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}