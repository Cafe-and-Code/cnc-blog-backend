using Blog.API.Commons;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddUserDTO
    {
        [MinLength(3, ErrorMessage = "Username must be between 3 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "Username must be between 3 and 50 characters long.")]
        public required string Username { get; set; }
        [MinLength(6, ErrorMessage = "Password must be between 6 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "Password must be between 6 and 50 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,50}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public required string Password { get; set; }
        [MinLength(6, ErrorMessage = "Password must be between 6 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "Password must be between 6 and 50 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,50}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
        [MinLength(5, ErrorMessage = "Full name must be between 5 and 100 characters long.")]
        [MaxLength(100, ErrorMessage = "Full name must be between 5 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public required string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DateOfBirth(ErrorMessage = "Date of birth cannot be a future date.")]
        public required DateOnly DateOfBirth { get; set; }
        public string? AvatarImageUrl { get; set; }
    }
}
