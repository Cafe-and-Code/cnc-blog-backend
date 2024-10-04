namespace Blog.API.Commons
{
    public static class Constants
    {
        /// <summary>
        /// Validate model Message For User
        /// </summary>
        public const string ErrorUsernameRequiredMessage = "Username is required.";
        public const string ErrorUsenameLengthMessage = "Username must be between 3 and 50 characters long.";
        public const string ErrorPasswordRequiredMessage = "Password is required.";
        public const string ErrorPasswordLengthMessage = "Password must be between 6 and 50 characters long.";
        public const string ErrorPasswordTypeMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
        public const string ErrorPasswordCompareMessage = "Passwords do not match.";
        public const string ErrorFullNameRequiredMessage = "Full Name is required.";
        public const string ErrorFullNameLengthMessage = "Full name must be between 5 and 100 characters long.";
        public const string ErrorFullNameTypeMessage = "Full name can only contain letters and spaces.";
        public const string ErrorEmailRequiredMessage = "Email is required.";
        public const string ErrorEmailTypeMessage = "Invalid email address.";
        public const string ErrorDateOfBirthRequiredMessage = "Date of birth is required.";
        public const string ErrorInvalidDateMessage = "Invalid date format.";
        public const string ErrorInvalidDateOfBirthMessage = "Date of birth cannot be in the future and must be at least 10 years old.";

        /// <summary>
        /// Authen User
        /// </summary>
        public const string InvalidUsernameOrPasswordMessage = "Invalid username or password.";
    }
}
