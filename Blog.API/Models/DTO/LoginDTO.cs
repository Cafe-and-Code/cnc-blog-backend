using Blog.API.Commons;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class LoginDTO
    {
        [MinLength(3, ErrorMessage = Constants.ErrorUsenameLengthMessage)]
        [MaxLength(50, ErrorMessage = Constants.ErrorUsenameLengthMessage)]
        [Required(ErrorMessage = Constants.ErrorUsernameRequiredMessage)]
        public string Username { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = Constants.ErrorPasswordLengthMessage)]
        [MaxLength(50, ErrorMessage = Constants.ErrorPasswordLengthMessage)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,50}$", ErrorMessage = Constants.ErrorPasswordTypeMessage)]
        [Required(ErrorMessage = Constants.ErrorPasswordRequiredMessage)]
        public string Password { get; set; } = string.Empty;
    }
}
