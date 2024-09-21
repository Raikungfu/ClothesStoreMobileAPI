using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        [Required]
        public string ShipName { get; set; }

        [Required]
        [EmailAddress]
        public string ShipMail { get; set; }

        [Required]
        public string ShipPhone { get; set; }

        [Required]
        public string ShipAddress { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now;

        public int? ShipFee { get; set; }

        public string? DiscountCode { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "direct";

        [Required]
        public string Status { get; set; } = "pending";

        public virtual Customer Customer { get; set; }
    }
}