using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly BloggieDbContext bloggieDbContext;
        public List<BlogPost> BlogPosts; 
        public ListModel(BloggieDbContext _bloggieDbContext)
        {
            bloggieDbContext = _bloggieDbContext;
        }
        public async Task OnGet()
        {
           BlogPosts=await bloggieDbContext.BlogPosts.ToListAsync();
        }
    }
}
