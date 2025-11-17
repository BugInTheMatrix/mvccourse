using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Minimum 6 Characters Required")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
