using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.CartItem
{
    public class CartItemCreateViewModel
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
