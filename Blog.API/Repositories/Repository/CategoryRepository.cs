using Blog.API.Data;
using Blog.API.Models.Domain;

namespace Blog.API.Repositories
{
    public class CategoryRepository(BlogDbContext dbContext) : BaseRepository<Categories>(dbContext), ICategoryRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
