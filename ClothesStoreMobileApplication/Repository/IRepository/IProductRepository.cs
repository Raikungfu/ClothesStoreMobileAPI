using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.ViewModels.Product;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
        void Update(Product product);

        IEnumerable<Product> GetProducts(bool orderByLatest = false, bool orderByMostDiscount = false, bool orderByMostSales = false, string includeProperties = null);

        ProductDetailViewModel GetProductDetail(int id);
    }
}
