using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.web.Pages.Admin.Blogs
{
    [Authorize(Roles ="Admin")]

    public class AddModel : PageModel
    {
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        private readonly IBlogPostRepository blogPostRepository;
        public AddModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository; 
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var blogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                Visible = AddBlogPostRequest.Visible,
                Author = AddBlogPostRequest.Author,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
            };
            await blogPostRepository.AddAsync(blogPost);
            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "New Blog created!"
            };
            TempData["Notification"] =JsonSerializer.Serialize(notification);
            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
