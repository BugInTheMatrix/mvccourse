namespace MyMvcApp.Models.ViewModels
{
    public class UsersViewModel
    {
        public List<User> users { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
