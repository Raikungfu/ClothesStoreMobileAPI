using ClothesStoreMobileApplication.Models;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IReplyReviewRepository : IRepository<ReplyReview>
    {
        public void Update(ReplyReview replyReview);
    }
}
