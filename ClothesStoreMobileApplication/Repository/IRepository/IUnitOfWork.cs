namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IOptionRepository Option { get; }
        IProductOptionRepository ProductOption { get; }
        IDiscountRepository Discount { get; }
        IChatRepository Chat { get; }
        IChatMessageRepository ChatMessage { get; }
        ISellerRepository Seller { get; }
        ICustomerRepository Customer { get; }
        ICartRepository Cart { get; }
        ICartItemRepository CartItem { get; }
        IReviewRepository Review { get; }
        IReplyReviewRepository ReplyReview { get; }
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        void Save();
    }
}
