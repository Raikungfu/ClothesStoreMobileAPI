using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class Seller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellerId { get; set; }

        public string? Avt { get; set; }

        public string? Cover { get; set; }

        [MaxLength(100)]
        public string? CompanyName { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}