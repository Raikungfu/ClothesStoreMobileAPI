using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}