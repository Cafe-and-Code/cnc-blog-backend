using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;

namespace Blog.API.Repositories.Repository
{
    public class UserRepository(BlogDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
