using ClothesStoreMobileApplication.Models;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        public void Update(Review review);
    }
}
