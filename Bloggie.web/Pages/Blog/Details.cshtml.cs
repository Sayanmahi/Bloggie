using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        public BlogPost BlogPost { get; set; }
        private readonly IBlogPostRepository blogPostRepository;
        public DetailsModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
           BlogPost=await blogPostRepository.GetAsync(urlHandle);
            return Page();
        }
    }
}
