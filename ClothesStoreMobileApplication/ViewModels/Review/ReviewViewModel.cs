using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Review
{
    public class ReviewViewModel
    {
        public int? ProductId { get; set; }


        public int? OrderId { get; set; }

        public int? CustomerId { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
