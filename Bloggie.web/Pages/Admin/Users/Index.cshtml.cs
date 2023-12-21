using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Users
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository rep;
        [BindProperty]
        public Guid SelectedUserId { get; set; }
        public List<User> Users { get; set; }
        [BindProperty]
        public AddUser AddUserRequest { get; set; }
        public IndexModel(IUserRepository rep)
        {
            this.rep = rep;
        }
        public async Task<IActionResult> OnGet()
        {
            await GetUsers();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = AddUserRequest.UserName,
                    Email = AddUserRequest.Email
                };
                var roles = new List<string> { "User" };
                if (AddUserRequest.AdminCheckbox == true)
                {
                    roles.Add("Admin");
                }
                var u = await rep.Add(identityUser, AddUserRequest.Password, roles);
                if (u)
                {
                    return RedirectToPage("/Admin/Users/Index");
                }
                return Page();
            }
            await GetUsers();
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            await rep.Delete(SelectedUserId);
            return RedirectToPage("/Admin/Users/Index");
        }
        private async Task GetUsers()
        {
            var users = await rep.GetAll();
            Users = new List<User>();
            foreach (var i in users)
            {
                Users.Add(new Models.ViewModels.User()
                {
                    Id = Guid.Parse(i.Id),
                    UserName = i.UserName,
                    Email = i.Email
                });
            }
        }
    }
}
