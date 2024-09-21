using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Seller Seller { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}