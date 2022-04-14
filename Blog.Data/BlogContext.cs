using Microsoft.EntityFrameworkCore;

using Blog.Data.Models;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }


        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public BlogContext() 
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Blogs.db;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}