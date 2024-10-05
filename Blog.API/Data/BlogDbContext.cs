using Blog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
