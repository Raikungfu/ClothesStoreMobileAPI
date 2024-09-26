using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
    {
        private readonly ClothesStoreContext _db;

        public ChatMessageRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
    }
}
