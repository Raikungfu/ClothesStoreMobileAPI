using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreMobileApplication.Models
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal DiscountPercentage { get; set; }

        public int? Quantity { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool Status { get; set; } = true;
    }
}
