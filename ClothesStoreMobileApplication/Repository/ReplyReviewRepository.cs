using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;

namespace ClothesStoreMobileApplication.Repository
{
    public class ReplyReviewRepository : Repository<ReplyReview>, IReplyReviewRepository
    {
        private readonly ClothesStoreContext _db;
        public ReplyReviewRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ReplyReview replyReview)
        {
            var objFromDb = _db.ReplyReviews.FirstOrDefault(s => s.ReplyId == replyReview.ReplyId);
            if (objFromDb != null)
            {
                objFromDb.Content = replyReview.Content;
                objFromDb.ReplyDate = replyReview.ReplyDate;
            }
        }
    }
}
