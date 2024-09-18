using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
