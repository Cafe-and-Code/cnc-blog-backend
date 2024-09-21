using System.Text;
using System.Security.Cryptography;
using System.Text;

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
}
