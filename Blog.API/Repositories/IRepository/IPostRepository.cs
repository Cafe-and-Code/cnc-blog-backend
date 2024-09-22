using Blog.API.Models.Domain;

namespace Blog.API.Repositories.IRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetAllAsync();
        new Task<Post> AddAsync(Post post);

        new Task<Post> UpdateAsync(Post post);
    }
}
