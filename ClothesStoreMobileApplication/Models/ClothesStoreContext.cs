using Microsoft.EntityFrameworkCore;

namespace ClothesStoreMobileApplication.Models
{
    public class ClothesStoreContext : DbContext
    {
        public ClothesStoreContext(DbContextOptions<ClothesStoreContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReplyReview> ReplyReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User1)
                .WithMany()
                .HasForeignKey(c => c.UserId1)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany()
                .HasForeignKey(c => c.UserId2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.User)
            .WithMany()
            .HasForeignKey(cm => cm.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Chat)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(cm => cm.RoomId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = "adminpassword", // Nên mã hóa mật khẩu
                    Email = "admin@example.com",
                    Phone = "0123456789",
                    Status = true,
                    UserType = UserType.Admin
                },
                new User
                {
                    UserId = 2,
                    Username = "seller1",
                    Password = "seller1password", // Nên mã hóa mật khẩu
                    Email = "seller1@example.com",
                    Phone = "0981961993",
                    Status = true,
                    UserType = UserType.Seller
                },
                new User
                {
                    UserId = 3,
                    Username = "customer1",
                    Password = "customer1password", // Nên mã hóa mật khẩu
                    Email = "customer1@example.com",
                    Phone = "0123987654",
                    Status = true,
                    UserType = UserType.Customer
                },
                new User
                {
                    UserId = 4,
                    Username = "seller2",
                    Password = "seller2password", // Nên mã hóa mật khẩu
                    Email = "seller2@example.com",
                    Phone = "0931335263",
                    Status = true,
                    UserType = UserType.Seller
                }

            );

            // Seed data for Admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Avt = "https://i.imgur.com/83aoGyM.gif",
                    Cover = "admin_cover.jpg",
                    UserId = 1 // Phải trùng với UserId trong bảng User
                }
            );

            // Seed data for Seller
            modelBuilder.Entity<Seller>().HasData(
                new Seller
                {
                    SellerId = 1,
                    Avt = "https://i.imgur.com/thZDKR1.png",
                    Cover = "https://i.imgur.com/nIBeNMD.jpeg",
                    CompanyName = "Tây Fashion Shop 1",
                    Address = "368 Nguyễn Thái Học, Ngô Mây, Thành phố, Bình Định, Vietnam",
                    Description = "Man Fashion \nA well-known fashion retailer offering high-quality garments.",
                    UserId = 2,
                    Latitude = 13.765308955609873,
                    Longitude = 109.21480116679632
                },
                new Seller
                {
                    SellerId = 2,
                    Avt = "https://i.imgur.com/7ZIv1jz.png",
                    Cover = "https://i.imgur.com/7ZIv1jz.png",
                    CompanyName = "Saigon Jane T-shirt Shop",
                    Address = "185-12 Phạm Ngũ Lão, Phường Phạm Ngũ Lão, Quận 1, Hồ Chí Minh 700000, Vietnam",
                    Description = "A leading shoe retailer offering a wide range of footwear.",
                    UserId = 4,
                    Latitude = 10.771346809594261,
                    Longitude = 106.6925437473553
                }
            );

            // Seed data for Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "John Doe",
                    Address = "456 Customer Avenue, City Y",
                    Avt = "https://images.unsplash.com/photo-1556745753-b2904692b3cd?q=80&w=1973&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    UserId = 3 // Tham chiếu tới người dùng có UserId = 3
                }
            );

            // Seed data for Category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "T-Shirts", Img = "https://plus.unsplash.com/premium_photo-1673356301535-2cc45bcc79e4?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Category { CategoryId = 2, Name = "Jeans", Img = "https://plus.unsplash.com/premium_photo-1674828601362-afb73c907ebe?q=80&w=1953&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Category { CategoryId = 3, Name = "Jackets", Img = "https://plus.unsplash.com/premium_photo-1676212689512-5b66701912d4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Category { CategoryId = 4, Name = "Shoes", Img = "https://plus.unsplash.com/premium_photo-1682435561654-20d84cef00eb?q=80&w=1918&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            );


            // Seed data for ProductOption
            modelBuilder.Entity<ProductOption>().HasData(
                new ProductOption { ProductOptionsId = 1, Name = "Size", NameDescription = "Small" },
                new ProductOption { ProductOptionsId = 2, Name = "Size", NameDescription = "Medium" },
                new ProductOption { ProductOptionsId = 3, Name = "Size", NameDescription = "Large" },
                new ProductOption { ProductOptionsId = 4, Name = "Color", NameDescription = "Blue" },
                new ProductOption { ProductOptionsId = 5, Name = "Color", NameDescription = "Red" },
                new ProductOption { ProductOptionsId = 6, Name = "Color", NameDescription = "Green" },
                new ProductOption { ProductOptionsId = 7, Name = "Material", NameDescription = "Cotton" },
                new ProductOption { ProductOptionsId = 8, Name = "Material", NameDescription = "Polyester" },
                new ProductOption { ProductOptionsId = 9, Name = "Material", NameDescription = "Wool" },
                new ProductOption { ProductOptionsId = 10, Name = "Style", NameDescription = "Casual" },
                new ProductOption { ProductOptionsId = 11, Name = "Style", NameDescription = "Formal" },
                new ProductOption { ProductOptionsId = 12, Name = "Length", NameDescription = "Short" },
                new ProductOption { ProductOptionsId = 13, Name = "Length", NameDescription = "Long" },
                new ProductOption { ProductOptionsId = 14, Name = "Fit", NameDescription = "Regular" },
                new ProductOption { ProductOptionsId = 15, Name = "Fit", NameDescription = "Slim" },
                new ProductOption { ProductOptionsId = 16, Name = "Season", NameDescription = "Summer" },
                new ProductOption { ProductOptionsId = 17, Name = "Season", NameDescription = "Winter" },
                new ProductOption { ProductOptionsId = 18, Name = "Gender", NameDescription = "Men" },
                new ProductOption { ProductOptionsId = 19, Name = "Gender", NameDescription = "Women" },
                new ProductOption { ProductOptionsId = 20, Name = "Origin", NameDescription = "Imported" }
            );

            // Seed data for Option
            modelBuilder.Entity<Option>().HasData(
                new Option { OptionId = 1, OptionGroupId = 1, ProductId = 1, Price = 1.0M },
                new Option { OptionId = 2, OptionGroupId = 2, ProductId = 1, Price = 1.5M },
                new Option { OptionId = 3, OptionGroupId = 3, ProductId = 1, Price = 2.0M },
                new Option { OptionId = 4, OptionGroupId = 4, ProductId = 1, Price = 2.5M },
                new Option { OptionId = 5, OptionGroupId = 1, ProductId = 2, Price = 1.0M },
                new Option { OptionId = 6, OptionGroupId = 2, ProductId = 2, Price = 1.8M },
                new Option { OptionId = 7, OptionGroupId = 3, ProductId = 2, Price = 2.2M },
                new Option { OptionId = 8, OptionGroupId = 5, ProductId = 2, Price = 2.7M },
                new Option { OptionId = 9, OptionGroupId = 1, ProductId = 3, Price = 1.3M },
                new Option { OptionId = 10, OptionGroupId = 4, ProductId = 3, Price = 3.0M },
                new Option { OptionId = 11, OptionGroupId = 6, ProductId = 3, Price = 3.5M },
                new Option { OptionId = 12, OptionGroupId = 7, ProductId = 3, Price = 4.0M },
                new Option { OptionId = 13, OptionGroupId = 8, ProductId = 4, Price = 4.5M },
                new Option { OptionId = 14, OptionGroupId = 9, ProductId = 4, Price = 5.0M },
                new Option { OptionId = 15, OptionGroupId = 10, ProductId = 4, Price = 5.5M },
                new Option { OptionId = 16, OptionGroupId = 2, ProductId = 5, Price = 1.5M },
                new Option { OptionId = 17, OptionGroupId = 3, ProductId = 5, Price = 2.0M },
                new Option { OptionId = 18, OptionGroupId = 5, ProductId = 5, Price = 2.5M },
                new Option { OptionId = 19, OptionGroupId = 6, ProductId = 6, Price = 3.0M },
                new Option { OptionId = 20, OptionGroupId = 7, ProductId = 6, Price = 3.5M },
                new Option { OptionId = 21, OptionGroupId = 1, ProductId = 7, Price = 1.0M },
                new Option { OptionId = 22, OptionGroupId = 8, ProductId = 7, Price = 4.0M },
                new Option { OptionId = 23, OptionGroupId = 9, ProductId = 7, Price = 4.5M },
                new Option { OptionId = 24, OptionGroupId = 2, ProductId = 8, Price = 1.5M },
                new Option { OptionId = 25, OptionGroupId = 10, ProductId = 8, Price = 5.5M },
                new Option { OptionId = 26, OptionGroupId = 3, ProductId = 8, Price = 2.0M },
                new Option { OptionId = 27, OptionGroupId = 1, ProductId = 9, Price = 1.0M },
                new Option { OptionId = 28, OptionGroupId = 6, ProductId = 9, Price = 3.5M },
                new Option { OptionId = 29, OptionGroupId = 7, ProductId = 9, Price = 4.0M },
                new Option { OptionId = 30, OptionGroupId = 5, ProductId = 10, Price = 2.5M },
                new Option { OptionId = 31, OptionGroupId = 8, ProductId = 10, Price = 4.5M },
                new Option { OptionId = 32, OptionGroupId = 9, ProductId = 10, Price = 5.0M },
                new Option { OptionId = 33, OptionGroupId = 10, ProductId = 11, Price = 5.5M },
                new Option { OptionId = 34, OptionGroupId = 2, ProductId = 11, Price = 1.7M },
                new Option { OptionId = 35, OptionGroupId = 3, ProductId = 11, Price = 2.3M },
                new Option { OptionId = 36, OptionGroupId = 4, ProductId = 12, Price = 2.9M },
                new Option { OptionId = 37, OptionGroupId = 5, ProductId = 12, Price = 3.4M },
                new Option { OptionId = 38, OptionGroupId = 7, ProductId = 13, Price = 4.4M },
                new Option { OptionId = 39, OptionGroupId = 8, ProductId = 13, Price = 4.9M },
                new Option { OptionId = 40, OptionGroupId = 9, ProductId = 14, Price = 5.4M },
                new Option { OptionId = 41, OptionGroupId = 10, ProductId = 14, Price = 5.9M },
                new Option { OptionId = 42, OptionGroupId = 6, ProductId = 15, Price = 3.8M },
                new Option { OptionId = 43, OptionGroupId = 2, ProductId = 15, Price = 1.9M },
                new Option { OptionId = 44, OptionGroupId = 4, ProductId = 16, Price = 2.6M },
                new Option { OptionId = 45, OptionGroupId = 8, ProductId = 16, Price = 4.7M },
                new Option { OptionId = 46, OptionGroupId = 1, ProductId = 17, Price = 1.2M },
                new Option { OptionId = 47, OptionGroupId = 3, ProductId = 17, Price = 2.5M },
                new Option { OptionId = 48, OptionGroupId = 5, ProductId = 18, Price = 3.1M },
                new Option { OptionId = 49, OptionGroupId = 6, ProductId = 18, Price = 3.7M },
                new Option { OptionId = 50, OptionGroupId = 7, ProductId = 19, Price = 4.2M },
                new Option { OptionId = 51, OptionGroupId = 8, ProductId = 19, Price = 4.8M },
                new Option { OptionId = 52, OptionGroupId = 9, ProductId = 20, Price = 5.3M },
                new Option { OptionId = 53, OptionGroupId = 10, ProductId = 20, Price = 5.7M },
                 new Option { OptionId = 54, OptionGroupId = 1, ProductId = 21, Price = 1.1M },
                new Option { OptionId = 55, OptionGroupId = 2, ProductId = 21, Price = 1.5M },
                new Option { OptionId = 56, OptionGroupId = 3, ProductId = 22, Price = 2.0M },
                new Option { OptionId = 57, OptionGroupId = 4, ProductId = 22, Price = 2.5M },
                new Option { OptionId = 58, OptionGroupId = 5, ProductId = 23, Price = 3.0M },
                new Option { OptionId = 59, OptionGroupId = 6, ProductId = 23, Price = 3.5M },
                new Option { OptionId = 60, OptionGroupId = 7, ProductId = 24, Price = 4.0M },
                new Option { OptionId = 61, OptionGroupId = 8, ProductId = 24, Price = 4.5M },
                new Option { OptionId = 62, OptionGroupId = 9, ProductId = 25, Price = 5.0M },
                new Option { OptionId = 63, OptionGroupId = 10, ProductId = 25, Price = 5.5M }
            );

            // Seed data for 10 Products in Category 1 (T-Shirts)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "T-Shirt 1", Img = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 1", NewPrice = 15, OldPrice = 20, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 2, Name = "T-Shirt 2", Img = "https://images.unsplash.com/photo-1485218126466-34e6392ec754?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 2", NewPrice = 18, OldPrice = 23, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 3, Name = "T-Shirt 3", Img = "https://plus.unsplash.com/premium_photo-1661373644394-ebc6f569826c?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 3", NewPrice = 20, OldPrice = 25, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 4, Name = "T-Shirt 4", Img = "https://images.unsplash.com/photo-1522706604291-210a56c3b376?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 4", NewPrice = 22, OldPrice = 27, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 5, Name = "T-Shirt 5", Img = "https://images.unsplash.com/photo-1592492135673-55966d3b541a?q=80&w=1986&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 5", NewPrice = 25, OldPrice = 30, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 6, Name = "T-Shirt 6", Img = "https://images.unsplash.com/photo-1617310208925-9ad2a0391d85?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 6", NewPrice = 28, OldPrice = 33, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 7, Name = "T-Shirt 7", Img = "https://plus.unsplash.com/premium_photo-1670088464876-e1fa625c8697?q=80&w=1886&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 7", NewPrice = 30, OldPrice = 35, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 8, Name = "T-Shirt 8", Img = "https://images.unsplash.com/photo-1581791538302-03537b9c97bf?q=80&w=1779&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 8", NewPrice = 32, OldPrice = 37, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 9, Name = "T-Shirt 9", Img = "https://plus.unsplash.com/premium_photo-1687850859076-b8e74a1ac8fa?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 9", NewPrice = 35, OldPrice = 40, QuantitySold = 0, CategoryId = 1, SellerId = 1 },
                new Product { ProductId = 10, Name = "T-Shirt 10", Img = "https://images.unsplash.com/photo-1521097769011-905fc0291094?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for T-Shirt 10", NewPrice = 37, OldPrice = 42, QuantitySold = 0, CategoryId = 1, SellerId = 1 }
            );

            // Seed data for 10 Products in Category 2 (Jeans) with Seller 4
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 11, Name = "Jeans 1", Img = "https://plus.unsplash.com/premium_photo-1674828601362-afb73c907ebe?q=80&w=1953&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 1", NewPrice = 30, OldPrice = 40, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 12, Name = "Jeans 2", Img = "https://images.unsplash.com/photo-1604176354204-9268737828e4?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 2", NewPrice = 32, OldPrice = 42, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 13, Name = "Jeans 3", Img = "https://images.unsplash.com/photo-1542272604-787c3835535d?q=80&w=1926&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 3", NewPrice = 34, OldPrice = 44, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 14, Name = "Jeans 4", Img = "https://images.unsplash.com/photo-1623120389902-6c846c80f4c8?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 4", NewPrice = 36, OldPrice = 46, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 15, Name = "Jeans 5", Img = "https://plus.unsplash.com/premium_photo-1675877946243-bc3f83e65afe?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 5", NewPrice = 38, OldPrice = 48, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 16, Name = "Jeans 6", Img = "https://images.unsplash.com/photo-1608613517869-07b097abbcf3?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 6", NewPrice = 40, OldPrice = 50, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 17, Name = "Jeans 7", Img = "https://images.unsplash.com/photo-1474570094496-a0e20f2e8050?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 7", NewPrice = 42, OldPrice = 52, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 18, Name = "Jeans 8", Img = "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 8", NewPrice = 44, OldPrice = 54, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 19, Name = "Jeans 9", Img = "https://plus.unsplash.com/premium_photo-1674828601017-2b8d4ea90aca?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 9", NewPrice = 46, OldPrice = 56, QuantitySold = 0, CategoryId = 2, SellerId = 2 },
                new Product { ProductId = 20, Name = "Jeans 10", Img = "https://images.unsplash.com/photo-1589561818145-eb2a4ba71a3c?q=80&w=2069&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jeans 10", NewPrice = 48, OldPrice = 58, QuantitySold = 0, CategoryId = 2, SellerId = 2 }
            );

            // Seed data for 5 Products in Category 3 (Jackets) with Seller 2
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 21, Name = "Jacket 1", Img = "https://plus.unsplash.com/premium_photo-1671030274122-b6ac34f87b8b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jacket 1", NewPrice = 50, OldPrice = 60, QuantitySold = 0, CategoryId = 3, SellerId = 2 },
                new Product { ProductId = 22, Name = "Jacket 2", Img = "https://images.unsplash.com/photo-1542327897-d73f4005b533?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jacket 2", NewPrice = 55, OldPrice = 65, QuantitySold = 0, CategoryId = 3, SellerId = 2 },
                new Product { ProductId = 23, Name = "Jacket 3", Img = "https://images.unsplash.com/photo-1576993537667-c6d2386f90a2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jacket 3", NewPrice = 58, OldPrice = 68, QuantitySold = 0, CategoryId = 3, SellerId = 2 },
                new Product { ProductId = 24, Name = "Jacket 4", Img = "https://images.unsplash.com/photo-1551318181-655e9748c0a6?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jacket 4", NewPrice = 60, OldPrice = 70, QuantitySold = 0, CategoryId = 3, SellerId = 2 },
                new Product { ProductId = 25, Name = "Jacket 5", Img = "https://plus.unsplash.com/premium_photo-1683121231638-4100d7f6deb2?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Jacket 5", NewPrice = 62, OldPrice = 72, QuantitySold = 0, CategoryId = 3, SellerId = 2 }
            );

            // Seed data for 5 Products in Category 4 (Shoes) with Seller 1
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 31, Name = "Shoe 1", Img = "https://plus.unsplash.com/premium_photo-1682435561654-20d84cef00eb?q=80&w=1918&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Shoe 1", NewPrice = 40, OldPrice = 50, QuantitySold = 0, CategoryId = 4, SellerId = 1 },
                new Product { ProductId = 32, Name = "Shoe 2", Img = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Shoe 2", NewPrice = 42, OldPrice = 52, QuantitySold = 0, CategoryId = 4, SellerId = 1 },
                new Product { ProductId = 33, Name = "Shoe 3", Img = "https://images.unsplash.com/photo-1511556532299-8f662fc26c06?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Shoe 3", NewPrice = 45, OldPrice = 55, QuantitySold = 0, CategoryId = 4, SellerId = 1 },
                new Product { ProductId = 34, Name = "Shoe 4", Img = "https://images.unsplash.com/photo-1561909848-977d0617f275?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Shoe 4", NewPrice = 48, OldPrice = 58, QuantitySold = 0, CategoryId = 4, SellerId = 1 },
                new Product { ProductId = 35, Name = "Shoe 5", Img = "https://plus.unsplash.com/premium_photo-1682125177822-63c27a3830ea?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Quantity = 100, Description = "Description for Shoe 5", NewPrice = 50, OldPrice = 60, QuantitySold = 0, CategoryId = 4, SellerId = 1 }
            );

            // Seed data for Discount
            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    DiscountId = 1,
                    Code = "SUMMER20",
                    Description = "20% off on summer collection",
                    DiscountPercentage = 20,
                    Quantity = 100,
                    StartDate = new DateTime(2024, 6, 1),
                    EndDate = new DateTime(2024, 8, 31),
                    Status = true
                },
                new Discount
                {
                    DiscountId = 2,
                    Code = "WINTER15",
                    Description = "15% off on winter collection",
                    DiscountPercentage = 15,
                    Quantity = null,
                    StartDate = new DateTime(2024, 12, 1),
                    EndDate = new DateTime(2025, 2, 28),
                    Status = true
                }
            );

            // Seed data for Cart
            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, CustomerId = 1 }
            );

            // Seed data for CartItem
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = 1, Quantity = 2, CartId = 1, ProductId = 1 }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    CustomerId = 1,
                    ShipName = "John Doe",
                    ShipMail = "john@example.com",
                    ShipPhone = "123-456-7890",
                    ShipAddress = "123 Main St, Cityville",
                    OrderDate = DateTime.Now,
                    ShipFee = 5,
                    DiscountCode = "SUMMER20",
                    TotalAmount = 100.00,
                    PaymentMethod = "credit_card",
                    Status = "pending"
                },
                new Order
                {
                    OrderId = 2,
                    CustomerId = 1,
                    ShipName = "Jane Smith",
                    ShipMail = "jane@example.com",
                    ShipPhone = "098-765-4321",
                    ShipAddress = "456 Elm St, Townsville",
                    OrderDate = DateTime.Now,
                    ShipFee = 0,
                    DiscountCode = null,
                    TotalAmount = 150.00,
                    PaymentMethod = "paypal",
                    Status = "completed"
                }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    OrderItemId = 1,
                    OrderId = 1,
                    ProductId = 1, // Assuming you have a product with ID 1
                    Note = "Please gift wrap",
                    Quantity = 2
                },
                new OrderItem
                {
                    OrderItemId = 2,
                    OrderId = 1,
                    ProductId = 1, // Assuming you have a product with ID 2
                    Note = null,
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderItemId = 3,
                    OrderId = 2,
                    ProductId = 1, // Assuming you have a product with ID 1
                    Note = "Urgent delivery",
                    Quantity = 3
                }
            );

            // Seed Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    ProductId = 1, // Ensure the Product exists
                    OrderId = 1,   // Ensure the Order exists
                    CustomerId = 1, // Ensure the Customer exists
                    Rating = 5,
                    Comment = "Excellent product!"
                },
                new Review
                {
                    ReviewId = 2,
                    ProductId = 1,
                    OrderId = 2,
                    CustomerId = 1,
                    Rating = 4,
                    Comment = "Very good, but could improve."
                }
            );

            // Seed ReplyReviews
            modelBuilder.Entity<ReplyReview>().HasData(
                new ReplyReview
                {
                    ReplyId = 1,
                    ReviewId = 1,
                    UserId = 1, // Ensure the User exists
                    Content = "Thank you for your feedback!",
                    ReplyDate = DateTime.Now
                },
                new ReplyReview
                {
                    ReplyId = 2,
                    ReviewId = 2,
                    UserId = 2,
                    Content = "We appreciate your input!",
                    ReplyDate = DateTime.Now
                }
            );

            // Seed Chats
            modelBuilder.Entity<Chat>().HasData(
                new Chat
                {
                    RoomId = 1,
                    UserId1 = 3, // Ensure the Customer exists
                    UserId2 = 2    // Ensure the Seller exists
                }
            );

            // Seed ChatMessages
            modelBuilder.Entity<ChatMessage>().HasData(
                new ChatMessage
                {
                    MessageId = 1,
                    RoomId = 1,
                    SenderId = 3, // Assuming the sender is a Customer
                    Content = "Hello, I have a question about my order.",
                    Timestamp = DateTime.Now
                },
                new ChatMessage
                {
                    MessageId = 2,
                    RoomId = 1,
                    SenderId = 2, // Assuming the sender is a Seller
                    Content = "Sure, how can I assist you?",
                    Timestamp = DateTime.Now
                },
                new ChatMessage
                {
                    MessageId = 3,
                    RoomId = 1,
                    SenderId = 2,
                    Content = "Is there anything specific you need help with?",
                    Timestamp = DateTime.Now
                }
            );
        }
    }
}