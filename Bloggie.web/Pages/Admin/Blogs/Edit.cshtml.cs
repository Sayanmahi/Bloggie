using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Bloggie.web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository db;
        [BindProperty]
        public EditBlogPostRequest BlogPost { get; set; }
        [BindProperty]
        [Required]
        public string Tags { get; set; }
        public EditModel(IBlogPostRepository bl)
        {
            db = bl;
        }
        public async Task OnGet(Guid Id)
        {
            var blogpostdomainmodel = await db.GetAsync(Id);
            if(blogpostdomainmodel.Tags != null && blogpostdomainmodel != null)
            {
                BlogPost = new EditBlogPostRequest()
                {
                   Id=blogpostdomainmodel.Id,
                   Heading=blogpostdomainmodel.Heading,
                   PageTitle=blogpostdomainmodel.PageTitle,
                   Content=blogpostdomainmodel.Content,
                   ShortDescription=blogpostdomainmodel.ShortDescription,
                   FeaturedImageUrl=blogpostdomainmodel.FeaturedImageUrl,
                   UrlHandle=blogpostdomainmodel.UrlHandle,
                   PublishedDate=blogpostdomainmodel.PublishedDate,
                   Author=blogpostdomainmodel.Author,
                   Visible=blogpostdomainmodel.Visible
                };
                Tags = string.Join(",", blogpostdomainmodel.Tags.Select(x => x.Name));
            }
        }
        public async Task<IActionResult> OnPostEdit()
        {
            Validateheading();
            if (ModelState.IsValid)
            {
                try
                {
                    var blogostdomainmodel = new BlogPost()
                    {
                        Id = BlogPost.Id,
                        Heading = BlogPost.Heading,
                        PageTitle = BlogPost.PageTitle,
                        Content = BlogPost.Content,
                        ShortDescription = BlogPost.ShortDescription,
                        FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                        UrlHandle = BlogPost.UrlHandle,
                        PublishedDate = BlogPost.PublishedDate,
                        Author = BlogPost.Author,
                        Visible = BlogPost.Visible,
                        Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))

                    };

                    await db.UpdateAsync(blogostdomainmodel);
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Blog updated successfully!",
                        Type = Enums.NotificationType.Success
                    };
                }
                catch (Exception e)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = "Something went wrong!",
                        Type = Enums.NotificationType.Error
                    };
                }
                return Page();
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
        private void Validateheading()
        {
            if(BlogPost.Heading != null)
            {
                //check for minimum length
                if(BlogPost.Heading.Length <10 || BlogPost.Heading.Length >72)
                {
                    ModelState.AddModelError("BlogPost.Heading", "Heading can only be between 10 and 72 characters");
                }
                //check for maximum length
            }
        }
    }
}
