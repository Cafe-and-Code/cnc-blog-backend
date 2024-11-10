using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;

namespace Blog.API.Repositories.Repository
{
    public class PostCategoryRepository : BaseRepository<PostCategory>, IPostCategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostCategoryRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
