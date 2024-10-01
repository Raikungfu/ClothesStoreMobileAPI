using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Product
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }

        public int? CustomerId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}