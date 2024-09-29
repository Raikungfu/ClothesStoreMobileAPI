using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using System;

namespace ClothesStoreMobileApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClothesStoreContext _db;
        public UnitOfWork(ClothesStoreContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
            Option = new OptionRepository(_db);
            ProductOption = new ProductOptionRepository(_db);
            Discount = new DiscountRepository(_db);
            Chat = new ChatRepository(_db);
            User = new UserRepository(_db);
            ChatMessage = new ChatMessageRepository(_db);
            Seller = new SellerRepository(_db);
            Customer = new CustomerRepository(_db);
            Cart = new CartRepository(_db);
            CartItem = new CartItemRepository(_db);
            Review = new ReviewRepository(_db);
            ReplyReview = new ReplyReviewRepository(_db);
        }

        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IOptionRepository Option { get; private set; }
        public IProductOptionRepository ProductOption { get; private set; }
        public IDiscountRepository Discount { get; private set; }
        public IChatRepository Chat { get; private set; }
        public IUserRepository User { get; private set; }
        public IChatMessageRepository ChatMessage { get; private set; }
        public ISellerRepository Seller { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public ICartRepository Cart { get; private set; }
        public ICartItemRepository CartItem { get; private set; }
        public IReviewRepository Review { get; private set; }
        public IReplyReviewRepository ReplyReview { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
