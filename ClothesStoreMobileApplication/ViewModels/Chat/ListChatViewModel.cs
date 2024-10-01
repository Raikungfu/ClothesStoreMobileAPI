namespace ClothesStoreMobileApplication.ViewModels.Chat
{
    public class ListChatViewModel
    {
        public int RoomId { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string LatestMessage { get; set; }

        public DateTime? LatestMessageTime { get; set; }

        public bool IsOnline { get; set; }

        public bool IsTyping { get; set; }

        public int UserId { get; set; }
    }
}
