namespace Blog.API.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? AvatarImageUrl { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
