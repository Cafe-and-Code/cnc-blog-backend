using Blog.API.Data;
using Blog.API.Models.Domain;

namespace Blog.API.Repositories
{
    public class UserRepository(BlogDbContext dbContext) : BaseRepository<Users>(dbContext), IUserRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
