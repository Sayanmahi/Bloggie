using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace Bloggie.web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        public BlogPost BlogPost { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogpostLikeRepository blogpostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public DetailsModel(IBlogPostRepository blogPostRepository,IBlogpostLikeRepository blogpostLikeRepository,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogpostLikeRepository = blogpostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
           BlogPost=await blogPostRepository.GetAsync(urlHandle);
            if(BlogPost != null) 
            {
                if(signInManager.IsSignedIn(User))
                {
                    var likes = await blogpostLikeRepository.GetLikesForBlog(BlogPost.Id);
                    var userId = userManager.GetUserId(User);
                    Liked=likes.Any(x => x.UserId == Guid.Parse(userId));

                }

                TotalLikes = await blogpostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
            }
            return Page();
        }
    }
}
