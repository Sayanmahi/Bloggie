using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LoginViewModel{get;set;}
        public SignInManager<IdentityUser> signInManager { get; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string ReturnUrl)
        {
            var signInResult=await signInManager.PasswordSignInAsync(LoginViewModel.UserName,LoginViewModel.Password,false,false);
            if(signInResult.Succeeded) 
            {
                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                {
                    return RedirectToPage(ReturnUrl);
                }
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Wrong Credentials!"
                };
                return Page();
            }
        }
    }
}
