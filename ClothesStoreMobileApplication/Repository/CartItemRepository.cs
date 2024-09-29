using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly ClothesStoreContext _db;
        public CartItemRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CartItem cartItem)
        {
            var objFromDb = _db.CartItems.FirstOrDefault(s => s.CartItemId == cartItem.CartItemId);
            if (objFromDb != null)
            {
                objFromDb.Quantity = cartItem.Quantity;
            }
        }
    }
}
