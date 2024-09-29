using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(ClothesStoreContext db) : base(db) { }
    }
}
