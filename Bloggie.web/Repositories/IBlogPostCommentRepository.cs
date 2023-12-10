using Bloggie.web.Models.Domain;
using Bloggie.Web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);
    }
}
