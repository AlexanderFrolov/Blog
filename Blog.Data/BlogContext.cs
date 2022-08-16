using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Blog.Data.Configs;

namespace Blog.Data
{
    public class BlogContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; } = null!;
      //  public DbSet<Tag> Tags { get; set; } = null!;
        //public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        //public DbSet<Role> Roles { get; set; } = null!;


        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }
        
        public BlogContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Blogs.db;");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TagConfiguration());
        }
    }
}