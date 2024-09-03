namespace Blog.API.Models.Domain
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? AvatarImageUrl { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
