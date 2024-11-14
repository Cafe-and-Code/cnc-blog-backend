using Blog.API.Commons;
using Blog.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Blog.API.Repositories.IRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Post?> FindOneAsync(Expression<Func<Post, bool>> predicate);
        new Task<Post> AddAsync(Post post);
        new Task<Post> UpdateAsync(Post post);
    }
}
