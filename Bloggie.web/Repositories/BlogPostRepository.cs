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
            var ex = await db.BlogPosts.FindAsync(Id);
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
            var d=await db.BlogPosts.Include(nameof(BlogPost.Tags)).ToListAsync();
            return d;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
        {
            var d = await (db.BlogPosts.Include(nameof(BlogPost.Tags)).Where(x=> x.Tags.Any(x=>x.Name== tagName))).ToListAsync();
            return d;
        }

        public async Task<BlogPost> GetAsync(Guid Id)
        {
           var d= await db.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.Id==Id);
            return d;
        }

        public async Task<BlogPost> GetAsync(string urlHandle)
        {
            return await db.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
            
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var ex =await db.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.Id==blogPost.Id);
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
                if(blogPost.Tags != null && blogPost.Tags.Any())
                {
                    //Delete the existing tags
                    db.Tag.RemoveRange(ex.Tags);
                    //Add new Tags
                }
                blogPost.Tags.ToList().ForEach(x => x.BlogPostId = ex.Id);
                await db.Tag.AddRangeAsync(blogPost.Tags);

            }
            await db.SaveChangesAsync();
            return ex;
        }
    }
}
