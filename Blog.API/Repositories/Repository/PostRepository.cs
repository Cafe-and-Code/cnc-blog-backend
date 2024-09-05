using Blog.API.Data;
using Blog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories
{
    public class PostRepository(BlogDbContext dbContext) : BaseRepository<Posts>(dbContext), IPostRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
