using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.User
{
    public class LoginModels
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
