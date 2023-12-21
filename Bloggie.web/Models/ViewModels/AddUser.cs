using System.ComponentModel.DataAnnotations;

namespace Bloggie.web.Models.ViewModels
{
    public class AddUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool AdminCheckbox { get; set; }
    }
}
