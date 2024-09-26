using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class ProductOptionRepository : Repository<ProductOption>, IProductOptionRepository
    {
        private readonly ClothesStoreContext _db;

        public ProductOptionRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductOption productOption)
        {
            var objFromDb = _db.ProductOptions.FirstOrDefault(s => s.ProductOptionsId == productOption.ProductOptionsId);
            if (objFromDb != null)
            {
                objFromDb.Name = productOption.Name;
            }
        }
    }
}
