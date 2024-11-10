using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;

namespace Blog.API.Repositories.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public CategoryRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public new async Task<Category> AddAsync(Category category)
        {
            var currentDateTime = DateTime.UtcNow;
            category.CreatedAt = currentDateTime;
            category.UpdatedAt = currentDateTime;
            return await base.AddAsync(category);
        }
    }
}
