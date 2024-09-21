using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetAllAsync();
    }
}
