using System.ComponentModel.DataAnnotations;

namespace ApplicationToSellThings.BlazorUI.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
