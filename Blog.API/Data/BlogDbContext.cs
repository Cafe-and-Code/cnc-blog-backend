using Blog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
