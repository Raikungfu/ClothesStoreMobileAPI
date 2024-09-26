using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ClothesStoreContext _db;

        public CategoryRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.CategoryId == category.CategoryId);
            if (objFromDb != null)
            {
                objFromDb.Img = category.Img;
            }
            objFromDb.Name = category.Name;
        }
    }
}
