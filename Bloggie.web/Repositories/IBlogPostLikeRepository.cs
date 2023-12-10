using Bloggie.web.Models.Domain;
using Bloggie.Web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface IBlogpostLikeRepository
    {
        Task<int> GetTotalLikesForBlog(Guid blogPostId);
        Task AddLikeForBlog(Guid blogPostId,Guid userId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    }
}
