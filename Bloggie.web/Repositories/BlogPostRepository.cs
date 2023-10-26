using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext db;
        public BlogPostRepository(BloggieDbContext _db)
        {
            db = _db; 
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await db.BlogPosts.AddAsync(blogPost);
            await db.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var ex = await db.BlogPosts.FindAsync(BlogPost.Id);
            if(ex != null)
            {
                db.BlogPosts.Remove(ex);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var d=await db.BlogPosts.ToListAsync();
            return d;
        }

        public async Task<BlogPost> GetAsync(Guid Id)
        {
           var d= await db.BlogPosts.FindAsync(Id);
            return d;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var ex = db.BlogPosts.Find(blogPost.Id);
            if (ex != null)
            {
                ex.Heading = blogPost.Heading;
                ex.PageTitle = blogPost.PageTitle;
                ex.Content = blogPost.Content;
                ex.ShortDescription = blogPost.ShortDescription;
                ex.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                ex.UrlHandle = blogPost.UrlHandle;
                ex.PublishedDate = blogPost.PublishedDate;
                ex.Author = blogPost.Author;
                ex.Visible = blogPost.Visible;
            }
            await db.SaveChangesAsync();
            return ex;
        }
    }
}
