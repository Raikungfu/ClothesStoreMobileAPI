using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ClothesStoreContext _db;
        public ReviewRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Review review)
        {
            var objFromDb = _db.Reviews.FirstOrDefault(s => s.ReviewId == review.ReviewId);
            if (objFromDb != null)
            {
                objFromDb.Rating = review.Rating;
                objFromDb.Comment = review.Comment;
            }
        }
    }
}
