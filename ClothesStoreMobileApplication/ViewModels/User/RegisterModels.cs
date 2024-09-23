using ClothesStoreMobileApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.User
{
    public class RegisterModels
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
    }
}
