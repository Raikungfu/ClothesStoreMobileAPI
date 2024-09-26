using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class SellerRepository : Repository<Seller>, ISellerRepository
    {
        public SellerRepository(ClothesStoreContext db) : base(db)
        {
        }
    }
}
