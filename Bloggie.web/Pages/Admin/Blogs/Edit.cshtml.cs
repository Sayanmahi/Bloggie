using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository db;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public EditModel(IBlogPostRepository bl)
        {
            db = bl;
        }
        public async Task OnGet(Guid Id)
        {
            BlogPost = await db.GetAsync(Id);
        }
        public async Task<IActionResult> OnPostEdit()
        {
            await db.UpdateAsync(BlogPost);
            
            return RedirectToPage("/Admin/Blogs/List");

        }
        public async Task<IActionResult> OnPostDelete()
        {
            var ex=await db.DeleteAsync(BlogPost.Id);
            if (ex)
                return RedirectToPage("/Admin/Blogs/List");

            return Page();
        }
    }
}
