using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClothesStoreMobileApplication.Migrations
{
    /// <inheritdoc />
    public partial class dbInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    ProductOptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.ProductOptionsId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<int>(type: "int", nullable: false),
                    UserId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId2",
                        column: x => x.UserId2,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Avt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.SellerId);
                    table.ForeignKey(
                        name: "FK_Sellers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Media = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Chats",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ShipName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipFee = table.Column<int>(type: "int", nullable: true),
                    DiscountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPrice = table.Column<long>(type: "bigint", nullable: false),
                    OldPrice = table.Column<long>(type: "bigint", nullable: true),
                    QuantitySold = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "SellerId");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionGroupId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_ProductOptions_OptionGroupId",
                        column: x => x.OptionGroupId,
                        principalTable: "ProductOptions",
                        principalColumn: "ProductOptionsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Options_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ReplyReviews",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyReviews", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_ReplyReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplyReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Img", "Name" },
                values: new object[,]
                {
                    { 1, "https://plus.unsplash.com/premium_photo-1673356301535-2cc45bcc79e4?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirts" },
                    { 2, "https://plus.unsplash.com/premium_photo-1674828601362-afb73c907ebe?q=80&w=1953&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans" },
                    { 3, "https://plus.unsplash.com/premium_photo-1676212689512-5b66701912d4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jackets" },
                    { 4, "https://plus.unsplash.com/premium_photo-1682435561654-20d84cef00eb?q=80&w=1918&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoes" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountId", "Code", "Description", "DiscountPercentage", "EndDate", "Quantity", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "SUMMER20", "20% off on summer collection", 20m, new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, "WINTER15", "15% off on winter collection", 15m, new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionsId", "Name" },
                values: new object[,]
                {
                    { 1, "Size" },
                    { 2, "Color" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Phone", "Status", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "adminpassword", "0123456789", true, 0, "admin" },
                    { 2, "seller1@example.com", "seller1password", "0987654321", true, 1, "seller1" },
                    { 3, "customer1@example.com", "customer1password", "0123987654", true, 2, "customer1" },
                    { 4, "seller2@example.com", "seller2password", "0987123456", true, 1, "seller2" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Avt", "Cover", "UserId" },
                values: new object[] { 1, "https://i.imgur.com/83aoGyM.gif", "admin_cover.jpg", 1 });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "RoomId", "UserId1", "UserId2" },
                values: new object[] { 1, 3, 2 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Avt", "Name", "UserId" },
                values: new object[] { 1, "456 Customer Avenue, City Y", "https://images.unsplash.com/photo-1556745753-b2904692b3cd?q=80&w=1973&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "John Doe", 3 });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "SellerId", "Address", "Avt", "CompanyName", "Cover", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Fashion Street, City X", "https://i.imgur.com/thZDKR1.png", "Fashion World", "https://i.imgur.com/nIBeNMD.jpeg", "A well-known fashion retailer offering high-quality garments.", 2 },
                    { 2, "456 Shoe Street, City Y", "https://i.imgur.com/7ZIv1jz.png", "Shoe Haven", "https://i.imgur.com/7ZIv1jz.png", "A leading shoe retailer offering a wide range of footwear.", 4 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CustomerId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ChatMessages",
                columns: new[] { "MessageId", "Content", "Icon", "Media", "RoomId", "SenderId", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Hello, I have a question about my order.", null, null, 1, 3, new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2456) },
                    { 2, "Sure, how can I assist you?", null, null, 1, 2, new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2458) },
                    { 3, "Is there anything specific you need help with?", null, null, 1, 2, new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2459) }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DiscountCode", "OrderDate", "PaymentMethod", "ShipAddress", "ShipFee", "ShipMail", "ShipName", "ShipPhone", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, "SUMMER20", new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2320), "credit_card", "123 Main St, Cityville", 5, "john@example.com", "John Doe", "123-456-7890", "pending", 100.0 },
                    { 2, 1, null, new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2324), "paypal", "456 Elm St, Townsville", 0, "jane@example.com", "Jane Smith", "098-765-4321", "completed", 150.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CartId", "CategoryId", "Description", "Img", "Name", "NewPrice", "OldPrice", "Quantity", "QuantitySold", "SellerId" },
                values: new object[,]
                {
                    { 1, null, 1, "Description for T-Shirt 1", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 1", 15L, 20L, 100, 0L, 1 },
                    { 2, null, 1, "Description for T-Shirt 2", "https://images.unsplash.com/photo-1485218126466-34e6392ec754?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 2", 18L, 23L, 100, 0L, 1 },
                    { 3, null, 1, "Description for T-Shirt 3", "https://plus.unsplash.com/premium_photo-1661373644394-ebc6f569826c?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 3", 20L, 25L, 100, 0L, 1 },
                    { 4, null, 1, "Description for T-Shirt 4", "https://images.unsplash.com/photo-1522706604291-210a56c3b376?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 4", 22L, 27L, 100, 0L, 1 },
                    { 5, null, 1, "Description for T-Shirt 5", "https://images.unsplash.com/photo-1592492135673-55966d3b541a?q=80&w=1986&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 5", 25L, 30L, 100, 0L, 1 },
                    { 6, null, 1, "Description for T-Shirt 6", "https://images.unsplash.com/photo-1617310208925-9ad2a0391d85?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 6", 28L, 33L, 100, 0L, 1 },
                    { 7, null, 1, "Description for T-Shirt 7", "https://plus.unsplash.com/premium_photo-1670088464876-e1fa625c8697?q=80&w=1886&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 7", 30L, 35L, 100, 0L, 1 },
                    { 8, null, 1, "Description for T-Shirt 8", "https://images.unsplash.com/photo-1581791538302-03537b9c97bf?q=80&w=1779&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 8", 32L, 37L, 100, 0L, 1 },
                    { 9, null, 1, "Description for T-Shirt 9", "https://plus.unsplash.com/premium_photo-1687850859076-b8e74a1ac8fa?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 9", 35L, 40L, 100, 0L, 1 },
                    { 10, null, 1, "Description for T-Shirt 10", "https://images.unsplash.com/photo-1521097769011-905fc0291094?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "T-Shirt 10", 37L, 42L, 100, 0L, 1 },
                    { 11, null, 2, "Description for Jeans 1", "https://plus.unsplash.com/premium_photo-1674828601362-afb73c907ebe?q=80&w=1953&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 1", 30L, 40L, 100, 0L, 2 },
                    { 12, null, 2, "Description for Jeans 2", "https://images.unsplash.com/photo-1604176354204-9268737828e4?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 2", 32L, 42L, 100, 0L, 2 },
                    { 13, null, 2, "Description for Jeans 3", "https://images.unsplash.com/photo-1542272604-787c3835535d?q=80&w=1926&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 3", 34L, 44L, 100, 0L, 2 },
                    { 14, null, 2, "Description for Jeans 4", "https://images.unsplash.com/photo-1623120389902-6c846c80f4c8?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 4", 36L, 46L, 100, 0L, 2 },
                    { 15, null, 2, "Description for Jeans 5", "https://plus.unsplash.com/premium_photo-1675877946243-bc3f83e65afe?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 5", 38L, 48L, 100, 0L, 2 },
                    { 16, null, 2, "Description for Jeans 6", "https://images.unsplash.com/photo-1608613517869-07b097abbcf3?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 6", 40L, 50L, 100, 0L, 2 },
                    { 17, null, 2, "Description for Jeans 7", "https://images.unsplash.com/photo-1474570094496-a0e20f2e8050?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 7", 42L, 52L, 100, 0L, 2 },
                    { 18, null, 2, "Description for Jeans 8", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 8", 44L, 54L, 100, 0L, 2 },
                    { 19, null, 2, "Description for Jeans 9", "https://plus.unsplash.com/premium_photo-1674828601017-2b8d4ea90aca?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 9", 46L, 56L, 100, 0L, 2 },
                    { 20, null, 2, "Description for Jeans 10", "https://images.unsplash.com/photo-1589561818145-eb2a4ba71a3c?q=80&w=2069&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jeans 10", 48L, 58L, 100, 0L, 2 },
                    { 21, null, 3, "Description for Jacket 1", "https://plus.unsplash.com/premium_photo-1671030274122-b6ac34f87b8b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jacket 1", 50L, 60L, 100, 0L, 2 },
                    { 22, null, 3, "Description for Jacket 2", "https://images.unsplash.com/photo-1542327897-d73f4005b533?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jacket 2", 55L, 65L, 100, 0L, 2 },
                    { 23, null, 3, "Description for Jacket 3", "https://images.unsplash.com/photo-1576993537667-c6d2386f90a2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jacket 3", 58L, 68L, 100, 0L, 2 },
                    { 24, null, 3, "Description for Jacket 4", "https://images.unsplash.com/photo-1551318181-655e9748c0a6?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jacket 4", 60L, 70L, 100, 0L, 2 },
                    { 25, null, 3, "Description for Jacket 5", "https://plus.unsplash.com/premium_photo-1683121231638-4100d7f6deb2?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Jacket 5", 62L, 72L, 100, 0L, 2 },
                    { 31, null, 4, "Description for Shoe 1", "https://plus.unsplash.com/premium_photo-1682435561654-20d84cef00eb?q=80&w=1918&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoe 1", 40L, 50L, 100, 0L, 1 },
                    { 32, null, 4, "Description for Shoe 2", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoe 2", 42L, 52L, 100, 0L, 1 },
                    { 33, null, 4, "Description for Shoe 3", "https://images.unsplash.com/photo-1511556532299-8f662fc26c06?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoe 3", 45L, 55L, 100, 0L, 1 },
                    { 34, null, 4, "Description for Shoe 4", "https://images.unsplash.com/photo-1561909848-977d0617f275?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoe 4", 48L, 58L, 100, 0L, 1 },
                    { 35, null, 4, "Description for Shoe 5", "https://plus.unsplash.com/premium_photo-1682125177822-63c27a3830ea?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Shoe 5", 50L, 60L, 100, 0L, 1 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "CartItemId", "CartId", "ProductId", "Quantity" },
                values: new object[] { 1, 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "Name", "OptionGroupId", "Price", "ProductId" },
                values: new object[,]
                {
                    { 1, "Small", 1, 0m, 1 },
                    { 2, "Medium", 1, 0m, 1 },
                    { 3, "Red", 2, 0m, 1 },
                    { 4, "Blue", 2, 0m, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "Note", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, "Please gift wrap", 1, 1, 2 },
                    { 2, null, 1, 1, 1 },
                    { 3, "Urgent delivery", 2, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Comment", "CustomerId", "OrderId", "ProductId", "Rating" },
                values: new object[,]
                {
                    { 1, "Excellent product!", 1, 1, 1, 5 },
                    { 2, "Very good, but could improve.", 1, 2, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "ReplyReviews",
                columns: new[] { "ReplyId", "Content", "ReplyDate", "ReviewId", "UserId" },
                values: new object[,]
                {
                    { 1, "Thank you for your feedback!", new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2409), 1, 1 },
                    { 2, "We appreciate your input!", new DateTime(2024, 10, 1, 14, 40, 56, 982, DateTimeKind.Local).AddTicks(2411), 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RoomId",
                table: "ChatMessages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId1",
                table: "Chats",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId2",
                table: "Chats",
                column: "UserId2");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionGroupId",
                table: "Options",
                column: "OptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                table: "Options",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartId",
                table: "Products",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyReviews_ReviewId",
                table: "ReplyReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyReviews_UserId",
                table: "ReplyReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ReplyReviews");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
