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
                .AsNoTracking()
                .Where(c => c.UserId1 == id || c.UserId2 == id)
                .Include(c => c.ChatMessages)
                .ToList();

            var userIds = chats.Select(c => c.UserId1 == id ? c.UserId2 : c.UserId1).Distinct().ToList();

            var users = _db.Users
                .Where(u => userIds.Contains(u.UserId))
                .Select(u => new
                {
                    u.UserId,
                    Name = u.UserType == UserType.Seller
                        ? _db.Sellers.Where(s => s.UserId == u.UserId).Select(s => s.CompanyName).FirstOrDefault()
                        : u.UserType == UserType.Customer
                            ? _db.Customers.Where(cus => cus.UserId == u.UserId).Select(cus => cus.Name).FirstOrDefault()
                            : u.Username,
                    Avatar = u.UserType == UserType.Seller
                        ? _db.Sellers.Where(s => s.UserId == u.UserId).Select(s => s.Avt).FirstOrDefault()
                        : _db.Customers.Where(cus => cus.UserId == u.UserId).Select(cus => cus.Avt).FirstOrDefault()
                })
                .ToList();

            var result = chats.Select(c => new ListChatViewModel
            {
                RoomId = c.RoomId,
                UserId = c.UserId1 == id ? c.UserId2 : c.UserId1,
                Name = users.FirstOrDefault(u => u.UserId == (c.UserId1 == id ? c.UserId2 : c.UserId1)).Name,
                Avatar = users.FirstOrDefault(u => u.UserId == (c.UserId1 == id ? c.UserId2 : c.UserId1)).Avatar,
                LatestMessage = c.ChatMessages.OrderByDescending(m => m.Timestamp).Select(m => m.Content).FirstOrDefault(),
                LatestMessageTime = c.ChatMessages.OrderByDescending(m => m.Timestamp).Select(m => m.Timestamp).FirstOrDefault(),
            })
            .Distinct()
            .ToList();

            return result;
        }


    }
}
