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

            var users = (from u in _db.Users
                         where userIds.Contains(u.UserId)
                         join s in _db.Sellers on u.UserId equals s.UserId into sellerGroup
                         from seller in sellerGroup.DefaultIfEmpty()
                         join c in _db.Customers on u.UserId equals c.UserId into customerGroup
                         from customer in customerGroup.DefaultIfEmpty()
                         join a in _db.Admins on u.UserId equals a.UserId into adminGroup
                         from admin in adminGroup.DefaultIfEmpty()
                         select new
                         {
                             u.UserId,
                             Name = u.UserType == UserType.Seller ? seller.CompanyName
                                  : u.UserType == UserType.Customer ? customer.Name
                                  : u.Username,
                             Avatar = u.UserType == UserType.Seller ? seller.Avt
                                    : u.UserType == UserType.Customer ? customer.Avt
                                    : admin.Avt
                         }).ToList();

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
                .OrderBy(c => c.LatestMessageTime)
                .Skip(id * 15)
                .Take(15)
                .ToList();

            return result;
        }


    }
}
