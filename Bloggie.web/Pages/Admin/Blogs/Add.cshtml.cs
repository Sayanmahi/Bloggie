using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public string Heading { get; set; }
        [BindProperty]
        public string PageTitle { get; set; }
        [BindProperty]
        public string Content { get; set; }
        [BindProperty]
        public string ShortDescription { get; set; }
        [BindProperty]
        public string FeaturedImageUrl { get; set; }
        [BindProperty]
        public string PublishedDate { get; set; }
        [BindProperty]
        public string Author { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
        }
    }
}
