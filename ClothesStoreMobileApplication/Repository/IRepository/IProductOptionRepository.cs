using ClothesStoreMobileApplication.Models;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IProductOptionRepository : IRepository<ProductOption>
    {
        void Update(ProductOption productOption);
    }
}
