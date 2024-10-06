using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Models
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptionId { get; set; }

        [ForeignKey("ProductOptions")]
        public int OptionGroupId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public decimal? Price { get; set; }

        public ProductOption ProductOptions { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}