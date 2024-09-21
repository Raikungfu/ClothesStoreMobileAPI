using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public virtual ProductOption ProductOptions { get; set; }
        public virtual Product Product { get; set; }
    }
}