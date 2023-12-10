using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext db;
        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            db= bloggieDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
          await db.BlogPostComment.AddAsync(blogPostComment);
          await  db.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
        {
                return await db.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
            
        }
    }
}
