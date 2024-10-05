using Blog.API.Commons;
using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

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
            return await _dbContext.Posts.Include("Author").Where(post => !post.IsDeleted && post.Status == (int)PostStatus.Public).OrderByDescending(post => post.CreatedAt).Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public new async Task<Post> AddAsync(Post post)
        {
            var currentDateTime = DateTime.UtcNow;
            post.UpdatedAt = currentDateTime;
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
