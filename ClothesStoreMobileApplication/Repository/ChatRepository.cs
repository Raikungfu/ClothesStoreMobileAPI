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
            var userIds = _db.Chats
                .Where(c => c.UserId1 == id || c.UserId2 == id)
                .SelectMany(c => new[] { c.UserId1, c.UserId2 })
                .Distinct()
                .ToList();

            var sellers = _db.Sellers
                .Where(s => userIds.Contains(s.UserId))
                .ToDictionary(s => s.UserId, s => new { s.CompanyName, s.Avt });

            var customers = _db.Customers
                .Where(cus => userIds.Contains(cus.UserId))
                .ToDictionary(cus => cus.UserId, cus => new { cus.Name, cus.Avt });

            var admins = _db.Admins
                .Where(a => userIds.Contains(a.UserId))
                .ToDictionary(a => a.UserId, a => new { FullName = "Admin", a.Avt });

            var chats = _db.Chats
                .AsNoTracking()
                .Where(c => c.UserId1 == id || c.UserId2 == id)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.ChatMessages)
                .Select(c => new ListChatViewModel
                {
                    RoomId = c.RoomId,
                    UserId = c.UserId1 == id ? c.UserId2 : c.UserId1,

                    Name = c.UserId1 == id
                        ? (c.User2.UserType == UserType.Admin && admins.ContainsKey(c.UserId2)
                            ? "Admin " + c.User2.Username
                            : c.User2.UserType == UserType.Seller && sellers.ContainsKey(c.UserId2)
                                ? sellers[c.UserId2].CompanyName
                                : c.User2.UserType == UserType.Customer && customers.ContainsKey(c.UserId2)
                                    ? customers[c.UserId2].Name
                                    : c.User2.Username)
                        : (c.User1.UserType == UserType.Admin && admins.ContainsKey(c.UserId1)
                            ? "Admin " + c.User1.Username
                            : c.User1.UserType == UserType.Seller && sellers.ContainsKey(c.UserId1)
                                ? sellers[c.UserId1].CompanyName
                                : c.User1.UserType == UserType.Customer && customers.ContainsKey(c.UserId1)
                                    ? customers[c.UserId1].Name
                                    : c.User1.Username),

                    Avatar = c.UserId1 == id
                        ? (c.User2.UserType == UserType.Admin && admins.ContainsKey(c.UserId2)
                            ? admins[c.UserId2].Avt
                            : c.User2.UserType == UserType.Seller && sellers.ContainsKey(c.UserId2)
                                ? sellers[c.UserId2].Avt
                                : customers.ContainsKey(c.UserId2)
                                    ? customers[c.UserId2].Avt
                                    : null)
                        : (c.User1.UserType == UserType.Admin && admins.ContainsKey(c.UserId1)
                            ? admins[c.UserId1].Avt
                            : c.User1.UserType == UserType.Seller && sellers.ContainsKey(c.UserId1)
                                ? sellers[c.UserId1].Avt
                                : customers.ContainsKey(c.UserId1)
                                    ? customers[c.UserId1].Avt
                                    : null),

                    LatestMessage = c.ChatMessages
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Content)
                        .FirstOrDefault(),

                    LatestMessageTime = c.ChatMessages
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Timestamp)
                        .FirstOrDefault(),
                })
                .ToList();

            return chats;
        }

    }
}
