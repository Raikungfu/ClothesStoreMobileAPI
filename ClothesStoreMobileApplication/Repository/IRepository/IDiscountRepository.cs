using ClothesStoreMobileApplication.Models;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        void Update(Discount discount);
    }
}
