namespace ClothesStoreMobileApplication.ViewModels.ChatMessage
{
    public class MessageResponseViewModel
    {
        public string Status { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public DateTime Timestamp { get; set; }

        public int RoomId { get; set; }

        public string? Media { get; set; }

        public string? Icon { get; set; }
    }
}
