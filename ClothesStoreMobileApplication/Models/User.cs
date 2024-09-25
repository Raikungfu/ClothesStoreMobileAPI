using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreMobileApplication.Models
{
    public enum UserType
    {
        Admin,
        Seller,
        Customer
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        public UserType UserType { get; set; } = UserType.Customer;
    }
}