namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IOptionRepository Option { get; }
        IProductOptionRepository ProductOption { get; }
        void Save();
    }
}
