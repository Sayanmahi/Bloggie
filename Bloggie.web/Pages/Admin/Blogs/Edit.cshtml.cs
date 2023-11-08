using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository db;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        public EditModel(IBlogPostRepository bl)
        {
            db = bl;
        }
        public async Task OnGet(Guid Id)
        {
            BlogPost = await db.GetAsync(Id);
            if(BlogPost.Tags !=null && BlogPost !=null)
            {
                Tags=string.Join(",", BlogPost.Tags.Select(x => x.Name));
            }
        }
        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                BlogPost.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }));
                await db.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Blog updated successfully!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch(Exception e)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong!",
                    Type = Enums.NotificationType.Error
                };
            }
            return Page();

        }
        public async Task<IActionResult> OnPostDelete()
        {
            var ex=await db.DeleteAsync(BlogPost.Id);
            if (ex)
            {
                var notification = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Blog Deleted!"
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}
