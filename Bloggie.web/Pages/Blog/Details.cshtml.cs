using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        public BlogPost BlogPost { get; set; }
        public int TotalLikes { get; set; }
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogpostLikeRepository blogpostLikeRepository;
        public DetailsModel(IBlogPostRepository blogPostRepository,IBlogpostLikeRepository blogpostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogpostLikeRepository = blogpostLikeRepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
           BlogPost=await blogPostRepository.GetAsync(urlHandle);
            if(BlogPost != null) 
            {
               TotalLikes= await blogpostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
            }
            return Page();
        }
    }
}
