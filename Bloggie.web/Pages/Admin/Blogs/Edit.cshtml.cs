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
        public void OnGet(Guid Id)
        {
            BlogPost = db.BlogPosts.Find(Id);
        }
        public IActionResult OnPostEdit()
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
            db.SaveChanges();
            return RedirectToPage("/Admin/Blogs/List");

        }
        public IActionResult OnPostDelete()
        {
            var ex = db.BlogPosts.Find(BlogPost.Id);
            if (ex != null)
            {
                db.BlogPosts.Remove(ex);
                db.SaveChanges();
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();
        }
    }
}
