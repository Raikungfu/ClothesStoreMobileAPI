using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClothesStoreMobileApplication.Migrations
{
    /// <inheritdoc />
    public partial class createdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Chats",
                columns: new[] { "RoomId", "CustomerId", "SellerId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DiscountCode", "OrderDate", "PaymentMethod", "ShipAddress", "ShipFee", "ShipMail", "ShipName", "ShipPhone", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, "SUMMER20", new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1356), "credit_card", "123 Main St, Cityville", 5, "john@example.com", "John Doe", "123-456-7890", "pending", 100.0 },
                    { 2, 1, null, new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1360), "paypal", "456 Elm St, Townsville", 0, "jane@example.com", "Jane Smith", "098-765-4321", "completed", 150.0 }
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
                table: "ChatMessages",
                columns: new[] { "MessageId", "Content", "Icon", "Media", "RoomId", "SenderId", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Hello, I have a question about my order.", null, null, 1, 1, new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1497) },
                    { 2, "Sure, how can I assist you?", null, null, 1, 2, new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1499) },
                    { 3, "Is there anything specific you need help with?", null, null, 1, 2, new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1501) }
                });

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
                    { 1, "Thank you for your feedback!", new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1445), 1, 1 },
                    { 2, "We appreciate your input!", new DateTime(2024, 9, 21, 21, 6, 35, 513, DateTimeKind.Local).AddTicks(1447), 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "CartItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "SellerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "SellerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
