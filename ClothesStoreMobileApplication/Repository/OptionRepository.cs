using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        private readonly ClothesStoreContext _db;

        public OptionRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Option option)
        {
            var objFromDb = _db.Options.FirstOrDefault(s => s.OptionId == option.OptionId);
            if (objFromDb != null)
            {
                objFromDb.Name = option.Name;
                objFromDb.Price = option.Price;
                objFromDb.ProductId = option.ProductId;
                objFromDb.OptionGroupId = option.OptionGroupId;
            }
        }
    }
}
