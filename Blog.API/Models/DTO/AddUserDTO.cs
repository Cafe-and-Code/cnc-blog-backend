using Blog.API.Commons;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddUserDTO
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

        [MinLength(6, ErrorMessage = Constants.ErrorPasswordLengthMessage)]
        [MaxLength(50, ErrorMessage = Constants.ErrorPasswordLengthMessage)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,50}$", ErrorMessage = Constants.ErrorPasswordTypeMessage)]
        [Compare("Password", ErrorMessage = Constants.ErrorPasswordCompareMessage)]
        [Required(ErrorMessage = Constants.ErrorPasswordLengthMessage)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = Constants.ErrorFullNameLengthMessage)]
        [MaxLength(100, ErrorMessage = Constants.ErrorFullNameLengthMessage)]
        ///[RegularExpression(@"^[^ÀÁÂÃÈÉÊẾÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêếìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỂưăạảấầẩẫậắằẳẵặẹẻẽềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s ]+$", ErrorMessage = Constants.ErrorFullNameTypeMessage)]
        [Required(ErrorMessage = Constants.ErrorFullNameRequiredMessage)]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = Constants.ErrorEmailTypeMessage)]
        [Required(ErrorMessage = Constants.ErrorEmailRequiredMessage)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date, ErrorMessage = Constants.ErrorInvalidDateMessage)]
        [DateOfBirth(ErrorMessage = Constants.ErrorInvalidDateOfBirthMessage)]
        [Required(ErrorMessage = Constants.ErrorDateOfBirthRequiredMessage)]
        public DateOnly DateOfBirth { get; set; }

        public string? AvatarImageUrl { get; set; }
    }
}
