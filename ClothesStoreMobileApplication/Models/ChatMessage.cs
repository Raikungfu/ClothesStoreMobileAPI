using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreMobileApplication.Models
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [ForeignKey("Chat")]
        public int RoomId { get; set; }

        [ForeignKey("User")]
        public int SenderId { get; set; }

        public string? Content { get; set; }

        public string? Media { get; set; }

        public string? Icon { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
