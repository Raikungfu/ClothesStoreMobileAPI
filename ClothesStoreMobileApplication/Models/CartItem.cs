using ClothesStoreMobileApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}

