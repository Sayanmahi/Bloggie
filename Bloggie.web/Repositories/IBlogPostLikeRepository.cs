namespace Bloggie.web.Repositories
{
    public interface IBlogpostLikeRepository
    {
        Task<int> GetTotalLikesForBlog(Guid blogPostId);
    }
}
