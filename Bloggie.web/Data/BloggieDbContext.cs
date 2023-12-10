using Bloggie.web.Models.Domain;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class BloggieDbContext:DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options): base(options)
        {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Bloggie;");
        }
    }
}
