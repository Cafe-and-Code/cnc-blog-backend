using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        new Task<Category> AddAsync(Category category);
    }
}
