using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        private readonly BloggieDbContext bloggieDbContext;
        public AddModel(BloggieDbContext _bloggieDbContext)
        {
            bloggieDbContext = _bloggieDbContext; 
        }
        public void OnGet()
        {
        }
        public void OnPost()
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
                PublishedDate= AddBlogPostRequest.PublishedDate
            };
             bloggieDbContext.BlogPosts.Add(blogPost);
             bloggieDbContext.SaveChanges();
        }
    }
}
