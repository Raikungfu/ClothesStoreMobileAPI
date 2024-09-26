namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IOptionRepository Option { get; }
        IProductOptionRepository ProductOption { get; }
        IDiscountRepository Discount { get; }
        void Save();
    }
}
