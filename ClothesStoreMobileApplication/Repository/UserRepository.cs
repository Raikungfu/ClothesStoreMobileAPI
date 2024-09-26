using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ClothesStoreContext db) : base(db)
        {
        }
    }
}
