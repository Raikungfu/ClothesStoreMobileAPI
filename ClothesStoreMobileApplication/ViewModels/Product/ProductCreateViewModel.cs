using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Img { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Description { get; set; }

        [Required]
        public uint NewPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? SellerId { get; set; }
    }
}
