using Blog.API.Commons;
using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.API.Repositories.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var skipResult = (pageNumber - 1) * pageSize;
            return await _dbContext.Posts
                .Include(post => post.Author)
                .Include(post => post.PostCategory!)
                    .ThenInclude(postCategory => postCategory.Category)
                .Where(post => !post.IsDeleted && post.Status == (int)PostStatus.Public)
                .OrderByDescending(post => post.CreatedAt)
                .Skip(skipResult)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Post?> FindOneAsync(Expression<Func<Post, bool>> predicate)
        {
            return await _dbContext.Posts.AsNoTracking()
                        .Include(post => post.Author)
                        .Include(post => post.PostCategory!)
                        .ThenInclude(postCategory => postCategory.Category)
                        .Where(post => !post.IsDeleted && post.Status == (int)PostStatus.Public)
                        .FirstOrDefaultAsync(predicate);
        }

        public new async Task<Post> AddAsync(Post post)
        {
            var currentDateTime = DateTime.UtcNow;
            post.CreatedAt = currentDateTime;
            post.UpdatedAt = currentDateTime;
            return await base.AddAsync(post);
        }

        public new async Task<Post> UpdateAsync(Post post)
        {
            var currentPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (currentPost == null)
            {
                return post;
            }

            currentPost.Title = post.Title;
            currentPost.Content = post.Content;
            currentPost.Status = post.Status;
            currentPost.UpdatedAt = DateTime.UtcNow;

            _dbContext.Set<Post>().Update(currentPost);
            await _dbContext.SaveChangesAsync();
            return currentPost;
        }
    }
}
