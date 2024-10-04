using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Commons
{
    public static class StringExtension
    {
        public static string EncryptPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool ValidatePassword(this string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }

    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
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
