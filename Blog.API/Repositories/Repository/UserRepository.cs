using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;

namespace Blog.API.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly BlogDbContext _dbContext;

        public UserRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
