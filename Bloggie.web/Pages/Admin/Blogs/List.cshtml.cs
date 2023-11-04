using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        public List<BlogPost> BlogPosts; 
        public ListModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet()
        {
           BlogPosts=(await blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
