using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreMobileApplication.ViewModels.ChatMessage
{
    public class ChatMessageViewModel
    {
        public int RoomId { get; set; }

        public int SenderId { get; set; }

        public string? Content { get; set; }

        public string? Media { get; set; }

        public string? Icon { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public bool? IsSender { get; set; }
    }
}
