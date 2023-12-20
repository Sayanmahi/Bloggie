using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register RegisterViewModel { get; set; }
        private readonly UserManager<IdentityUser> userManager;
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager=userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = RegisterViewModel.UserName,
                    Email = RegisterViewModel.Email
                };
                var identityResult = await userManager.CreateAsync(user, RegisterViewModel.Password);

                if (identityResult.Succeeded)
                {
                    var addRolesResult = await userManager.AddToRoleAsync(user, "User");
                    if (addRolesResult.Succeeded)
                    {
                        ViewData["Notification"] = new Notification
                        {
                            Type = Enums.NotificationType.Success,
                            Message = "User Registered Successfully!"
                        };
                    }

                    return Page();
                }
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Something went Wrong"
                };
                return Page();
            }
            else
            return Page();
        }
    }
}
