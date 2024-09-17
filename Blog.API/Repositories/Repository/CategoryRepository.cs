using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;

namespace Blog.API.Repositories.Repository
{
    public class CategoryRepository(BlogDbContext dbContext) : IBaseRepository<Category>(dbContext), ICategoryRepository
    {
        private readonly BlogDbContext dbContext;
    }
}
