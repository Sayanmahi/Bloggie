using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
            var notifcationJson = (string)TempData["Notification"];
            if (notifcationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notifcationJson);
            }
            
           BlogPosts=(await blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
