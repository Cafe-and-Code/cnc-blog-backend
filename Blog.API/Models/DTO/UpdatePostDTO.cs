namespace Blog.API.Models.DTO
{
    public class UpdatePostDTO
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public List<string>? Categories { get; set; }
        public int Status { get; set; }
    }
}
