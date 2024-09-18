namespace Blog.API.Models.DTO
{
    public class UpdatePostDTO
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string CategoryIds { get; set; }
        public Guid AuthorId { get; set; }
        public int Status { get; set; }
    }
}
