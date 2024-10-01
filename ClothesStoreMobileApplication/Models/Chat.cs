using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Models
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [ForeignKey("User1")]
        public int UserId1 { get; set; }

        [ForeignKey("User2")]
        public int UserId2 { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}