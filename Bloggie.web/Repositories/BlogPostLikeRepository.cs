
using Bloggie.web.Data;
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
        public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
        {
           return await bloggieDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
