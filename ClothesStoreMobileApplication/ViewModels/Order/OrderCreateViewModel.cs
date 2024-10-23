using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Order
{
     public class OrderCreateViewModel
    {
        public string? DiscountCode { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "direct";
    }
}
