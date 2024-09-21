namespace Blog.API.Models.DTO
{
    public class UpdateUserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public string? AvatarImageUrl { get; set; }
    }
}
