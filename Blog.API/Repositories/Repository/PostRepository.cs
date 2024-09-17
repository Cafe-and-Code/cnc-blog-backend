using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Repository
{
    public class PostRepository(BlogDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
