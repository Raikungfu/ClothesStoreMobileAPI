using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClothesStoreMobileApplication.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7406));

            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7411));

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "Price",
                value: 1.0m);

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 1.5m, 1 });

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 2.0m, 1 });

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 2.5m, 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "OptionGroupId", "Price", "ProductId" },
                values: new object[,]
                {
                    { 5, 1, 1.0m, 2 },
                    { 6, 2, 1.8m, 2 },
                    { 7, 3, 2.2m, 2 },
                    { 9, 1, 1.3m, 3 },
                    { 10, 4, 3.0m, 3 },
                    { 16, 2, 1.5m, 5 },
                    { 17, 3, 2.0m, 5 },
                    { 21, 1, 1.0m, 7 },
                    { 24, 2, 1.5m, 8 },
                    { 26, 3, 2.0m, 8 },
                    { 27, 1, 1.0m, 9 },
                    { 34, 2, 1.7m, 11 },
                    { 35, 3, 2.3m, 11 },
                    { 36, 4, 2.9m, 12 },
                    { 43, 2, 1.9m, 15 },
                    { 44, 4, 2.6m, 16 },
                    { 46, 1, 1.2m, 17 },
                    { 47, 3, 2.5m, 17 },
                    { 54, 1, 1.1m, 21 },
                    { 55, 2, 1.5m, 21 },
                    { 56, 3, 2.0m, 22 },
                    { 57, 4, 2.5m, 22 }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7199));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7205));

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 3,
                columns: new[] { "Name", "NameDescription" },
                values: new object[] { "Size", "Large" });

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 4,
                column: "NameDescription",
                value: "Blue");

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionsId", "Name", "NameDescription" },
                values: new object[,]
                {
                    { 5, "Color", "Red" },
                    { 6, "Color", "Green" },
                    { 7, "Material", "Cotton" },
                    { 8, "Material", "Polyester" },
                    { 9, "Material", "Wool" },
                    { 10, "Style", "Casual" },
                    { 11, "Style", "Formal" },
                    { 12, "Length", "Short" },
                    { 13, "Length", "Long" },
                    { 14, "Fit", "Regular" },
                    { 15, "Fit", "Slim" },
                    { 16, "Season", "Summer" },
                    { 17, "Season", "Winter" },
                    { 18, "Gender", "Men" },
                    { 19, "Gender", "Women" },
                    { 20, "Origin", "Imported" }
                });

            migrationBuilder.UpdateData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 1,
                column: "ReplyDate",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 2,
                column: "ReplyDate",
                value: new DateTime(2024, 10, 17, 7, 56, 40, 545, DateTimeKind.Local).AddTicks(7331));

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "OptionGroupId", "Price", "ProductId" },
                values: new object[,]
                {
                    { 8, 5, 2.7m, 2 },
                    { 11, 6, 3.5m, 3 },
                    { 12, 7, 4.0m, 3 },
                    { 13, 8, 4.5m, 4 },
                    { 14, 9, 5.0m, 4 },
                    { 15, 10, 5.5m, 4 },
                    { 18, 5, 2.5m, 5 },
                    { 19, 6, 3.0m, 6 },
                    { 20, 7, 3.5m, 6 },
                    { 22, 8, 4.0m, 7 },
                    { 23, 9, 4.5m, 7 },
                    { 25, 10, 5.5m, 8 },
                    { 28, 6, 3.5m, 9 },
                    { 29, 7, 4.0m, 9 },
                    { 30, 5, 2.5m, 10 },
                    { 31, 8, 4.5m, 10 },
                    { 32, 9, 5.0m, 10 },
                    { 33, 10, 5.5m, 11 },
                    { 37, 5, 3.4m, 12 },
                    { 38, 7, 4.4m, 13 },
                    { 39, 8, 4.9m, 13 },
                    { 40, 9, 5.4m, 14 },
                    { 41, 10, 5.9m, 14 },
                    { 42, 6, 3.8m, 15 },
                    { 45, 8, 4.7m, 16 },
                    { 48, 5, 3.1m, 18 },
                    { 49, 6, 3.7m, 18 },
                    { 50, 7, 4.2m, 19 },
                    { 51, 8, 4.8m, 19 },
                    { 52, 9, 5.3m, 20 },
                    { 53, 10, 5.7m, 20 },
                    { 58, 5, 3.0m, 23 },
                    { 59, 6, 3.5m, 23 },
                    { 60, 7, 4.0m, 24 },
                    { 61, 8, 4.5m, 24 },
                    { 62, 9, 5.0m, 25 },
                    { 63, 10, 5.5m, 25 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4985));

            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4987));

            migrationBuilder.UpdateData(
                table: "ChatMessages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 0m, 2 });

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 0m, 3 });

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                columns: new[] { "Price", "ProductId" },
                values: new object[] { 0m, 4 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 3,
                columns: new[] { "Name", "NameDescription" },
                values: new object[] { "Color", "Blue" });

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionsId",
                keyValue: 4,
                column: "NameDescription",
                value: "Red");

            migrationBuilder.UpdateData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 1,
                column: "ReplyDate",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4917));

            migrationBuilder.UpdateData(
                table: "ReplyReviews",
                keyColumn: "ReplyId",
                keyValue: 2,
                column: "ReplyDate",
                value: new DateTime(2024, 10, 6, 13, 17, 7, 347, DateTimeKind.Local).AddTicks(4919));
        }
    }
}
