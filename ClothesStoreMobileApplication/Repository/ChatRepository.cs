using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly ClothesStoreContext _db;

        public ChatRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
    }
}
