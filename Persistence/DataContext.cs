// Purpose: DataContext class to connect to the database.
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}