using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Option
{
    public class OptionViewModel
    {
        public int OptionGroupId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

    }
}
