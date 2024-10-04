using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, string roles);
    }
}
