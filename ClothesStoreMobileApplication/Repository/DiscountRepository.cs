using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly ClothesStoreContext _db;

        public DiscountRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Discount discount)
        {
            var objFromDb = _db.Discounts.FirstOrDefault(s => s.DiscountId == discount.DiscountId);
            if (objFromDb != null)
            {
                objFromDb.Code = discount.Code;
                objFromDb.DiscountPercentage = discount.DiscountPercentage;
                objFromDb.Description = discount.Description;
                objFromDb.StartDate = discount.StartDate;
                objFromDb.EndDate = discount.EndDate;
                objFromDb.Quantity = discount.Quantity;
                objFromDb.Status = discount.Status; 
            }
        }
    }
}
