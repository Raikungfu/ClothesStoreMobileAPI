using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class ReplyReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplyId { get; set; }

        [ForeignKey("Review")]
        public int ReviewId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime ReplyDate { get; set; } = DateTime.Now;

        public virtual Review Review { get; set; }
        public virtual User User { get; set; }
    }
}