using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }


        [ForeignKey("Order")]
        public int? OrderId { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        [Required]
        public int Rating { get; set; } 

        public string? Comment { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
