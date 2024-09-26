using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class ProductOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOptionsId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public virtual ICollection<Option> Options { get; set; }
    }
}