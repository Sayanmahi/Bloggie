using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly BloggieDbContext db;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public EditModel(BloggieDbContext bl)
        {
            db = bl;
        }
        public async Task OnGet(Guid Id)
        {
            BlogPost = await db.BlogPosts.FindAsync(Id);
        }
        public async Task<IActionResult> OnPostEdit()
        {
            var ex=db.BlogPosts.Find(BlogPost.Id);
            if (ex != null)
            {
                ex.Heading = BlogPost.Heading;
                ex.PageTitle = BlogPost.PageTitle;
                ex.Content = BlogPost.Content;
                ex.ShortDescription = BlogPost.ShortDescription;
                ex.FeaturedImageUrl= BlogPost.FeaturedImageUrl;
                ex.UrlHandle= BlogPost.UrlHandle;
                ex.PublishedDate= BlogPost.PublishedDate;
                ex.Author= BlogPost.Author;
                ex.Visible= BlogPost.Visible;
            }
            await db.SaveChangesAsync();
            return RedirectToPage("/Admin/Blogs/List");

        }
        public async Task<IActionResult> OnPostDelete()
        {
            var ex =await db.BlogPosts.FindAsync(BlogPost.Id);
            if (ex != null)
            {
                db.BlogPosts.Remove(ex);
                await db.SaveChangesAsync();
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();
        }
    }
}
