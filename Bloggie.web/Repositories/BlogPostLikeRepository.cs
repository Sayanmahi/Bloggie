
using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostLikeRepository : IBlogpostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddLikeForBlog(Guid blogPostId, Guid userId)
        {
            var like = new BlogPostLike
            {
                Id = Guid.NewGuid(),
                BlogPostId = blogPostId,
                UserId = userId
            };
            await bloggieDbContext.BlogPostLike.AddAsync(like);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
           return await bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
        {
           return await bloggieDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
