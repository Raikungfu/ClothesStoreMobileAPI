using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.ReplyReview
{
    public class ReplyReviewViewModel
    {
        public int ReviewId { get; set; }
        
        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime ReplyDate { get; set; } = DateTime.Now;
    }
}
