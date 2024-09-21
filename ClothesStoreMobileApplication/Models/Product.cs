using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Img { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Description { get; set; }

        [Required]
        public uint NewPrice { get; set; }

        public uint? OldPrice { get; set; }

        public uint QuantitySold { get; set; } = 0;

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("Seller")]
        public int? SellerId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Seller Seller { get; set; }
        [JsonIgnore]
        public virtual ICollection<Option> Options { get; set; }
    }
}