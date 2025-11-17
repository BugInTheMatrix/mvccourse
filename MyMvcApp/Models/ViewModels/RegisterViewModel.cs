using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Minimum 6 Characters Required")]
        public string Password { get; set; }
    }
}
