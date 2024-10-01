using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Commons
{
    public static class StringExtension
    {
        public static string EncryptPassword(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedBytes = sha256.ComputeHash(passwordBytes);
                var hashedPassword = Encoding.UTF8.GetString(hashedBytes);
                return hashedPassword;
            }
        }
    }

    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateOnly dateOfBirth = (DateOnly)value;
                if (dateOfBirth.ToDateTime(TimeOnly.MinValue) > DateTime.UtcNow)
                {
                    return new ValidationResult("Date of birth cannot be a future date.");
                }
                else if (DateTime.UtcNow.Year - dateOfBirth.Year < 10)
                {
                    return new ValidationResult("Date of birth must be at least 10 years old.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
