using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Chat;
using Microsoft.EntityFrameworkCore;

namespace ClothesStoreMobileApplication.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly ClothesStoreContext _db;

        public ChatRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }

        public List<ListChatViewModel> GetChat(int id)
        {
            var chats = _db.Chats
                .Where(c => c.UserId1 == id || c.UserId2 == id)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.ChatMessages)
                .Select(c => new ListChatViewModel
                {
                    RoomId = c.RoomId,
                    UserId = c.UserId1 == id ? c.UserId2 : c.UserId1,
                    Name = c.UserId1 == id ? (c.User2.UserType == UserType.Seller 
                    ? _db.Sellers.Where(s => s.UserId == c.UserId2).Select(s => s.CompanyName).FirstOrDefault() 
                    : c.User2.UserType == UserType.Customer ? _db.Customers.Where(cus => cus.UserId == c.UserId2).Select(cus => cus.Name).FirstOrDefault() : c.User2.Username) 
                    : (c.User1.UserType == UserType.Seller ? _db.Sellers.Where(s => s.UserId == c.UserId1).Select(s => s.CompanyName).FirstOrDefault() 
                    : c.User1.UserType == UserType.Customer ? _db.Customers.Where(cus => cus.UserId == c.UserId1).Select(cus => cus.Name).FirstOrDefault() : c.User1.Username),

                    Avatar = c.UserId1 == id ? (c.User2.UserType == UserType.Seller
                    ? _db.Sellers.Where(s => s.UserId == c.UserId2).Select(s => s.Avt).FirstOrDefault() 
                    : _db.Customers.Where(cus => cus.UserId == c.UserId2).Select(cus => cus.Avt).FirstOrDefault())
                    : (c.User1.UserType == UserType.Seller ? _db.Sellers.Where(s => s.UserId == c.UserId1).Select(s => s.Avt).FirstOrDefault() 
                    : _db.Customers.Where(cus => cus.UserId == c.UserId1).Select(cus => cus.Avt).FirstOrDefault()),

                    LatestMessage = c.ChatMessages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Content,
                    LatestMessageTime = c.ChatMessages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp,
                }).ToList();

            return chats;
        }
    }
}
