using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebDienThoai.Migrations
{
    /// <inheritdoc />
    public partial class hung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3213E83FE65758FB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    passwordhash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Customer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83FC1F34C1D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    saleprice = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    isOnSale = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    imageUrl = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3213E83FB57B5FD7", x => x.id);
                    table.ForeignKey(
                        name: "FK__Product__categor__403A8C7D",
                        column: x => x.categoryid,
                        principalTable: "Category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    orderdate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chờ xử lý"),
                    paymentstatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chưa thanh toán"),
                    totalprice = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    shipname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    shipaddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipphone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3213E83F67510456", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__userid__49C3F6B7",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__B40CC6CD941DC666", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK__Inventory__Produ__440B1D61",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unitprice = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__3ADF45A617256A6F", x => new { x.orderid, x.productid });
                    table.ForeignKey(
                        name: "FK__OrderItem__order__4CA06362",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderItem__produ__4D94879B",
                        column: x => x.productid,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "id", "name", "slug" },
                values: new object[,]
                {
                    { 1, "Apple", "apple" },
                    { 2, "Samsung", "samsung" },
                    { 3, "Xiaomi", "xiaomi" },
                    { 4, "Phụ Kiện", "phu-kien" },
                    { 5, "Tai Nghe", "tai-nghe" },
                    { 6, "Ốp Lưng", "op-lung" },
                    { 7, "Dây Sạc", "day-sac" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "email", "fullname", "passwordhash", "role", "username" },
                values: new object[,]
                {
                    { 1, "admin@theking.vn", "Quản Trị Viên", "admin123", "Admin", "admin" },
                    { 2, "khachhang@gmail.com", "Nguyễn Văn A", "123456", "Customer", "khachhang" },
                    { 3, "tranthib@gmail.com", "Trần Thị B", "123456", "Customer", "khachhang2" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "id", "orderdate", "paymentstatus", "shipaddress", "shipname", "shipphone", "status", "totalprice", "userid" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chưa thanh toán", "123 Lê Lợi, Q1, HCM", "Nguyễn Văn A", "0909123456", "Đang xử lý", 33080000m, 2 },
                    { 2, new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đã thanh toán", "456 Nguyễn Huệ, Q1, HCM", "Trần Thị B", "0912345678", "Đã giao", 5490000m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isOnSale", "isPublished", "name", "price", "saleprice" },
                values: new object[] { 1, 1, "Titanium, Chip A17 Pro mạnh mẽ.", "/img/10.jpg", true, true, "iPhone 15 Pro", 28990000m, 27590000m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isPublished", "name", "price", "saleprice" },
                values: new object[,]
                {
                    { 2, 2, "Camera AI đột phá, màn hình Dynamic AMOLED 2X.", "/img/20.jpg", true, "Samsung S25", 27500000m, 27500000m },
                    { 3, 1, "Thiết kế bền bỉ, Camera kép tiên tiến.", "/img/9.jpg", true, "Iphone 13", 22990000m, 22990000m }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isOnSale", "isPublished", "name", "price", "saleprice" },
                values: new object[] { 4, 1, "Pin trâu, hiệu năng ổn định.", "/img/19.jpg", true, true, "iPhone 14", 13790000m, 12990000m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isPublished", "name", "price", "saleprice" },
                values: new object[,]
                {
                    { 5, 1, "Công nghệ tương lai, thiết kế siêu mỏng.", "/img/iphone-17.jpg", true, "iPhone 17", 34990000m, 34990000m },
                    { 6, 3, "Hợp tác cùng Leica, nhiếp ảnh đỉnh cao.", "/img/17.jpg", true, "Xiaomi 14 Ultra", 21990000m, 21990000m }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isOnSale", "isPublished", "name", "price", "saleprice" },
                values: new object[] { 7, 5, "Chống ồn chủ động, âm thanh không gian.", "/img/14.jpg", true, true, "AirPods Pro 2", 6190000m, 5490000m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryid", "Description", "imageUrl", "isPublished", "name", "price", "saleprice" },
                values: new object[,]
                {
                    { 8, 6, "Chống sốc chuẩn quân đội.", "/img/22.jpg", true, "Ốp lưng Spigen", 750000m, 750000m },
                    { 9, 7, "Siêu bền, hỗ trợ sạc nhanh 100W.", "/img/26.jpg", true, "Cáp Type C Anker", 350000m, 350000m },
                    { 10, 6, "Trong suốt, không ố vàng.", "/img/21.jpg", true, "Ốp lưng iPhone 15", 450000m, 450000m }
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "ProductId", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 50 },
                    { 2, 20 },
                    { 3, 15 },
                    { 4, 100 },
                    { 5, 5 },
                    { 6, 30 },
                    { 7, 200 },
                    { 8, 500 },
                    { 9, 300 },
                    { 10, 150 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "orderid", "productid", "quantity", "unitprice" },
                values: new object[,]
                {
                    { 1, 1, 1, 27590000m },
                    { 1, 7, 1, 5490000m },
                    { 2, 7, 1, 5490000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_userid",
                table: "Order",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_productid",
                table: "OrderItem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_categoryid",
                table: "Product",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616496D0E9A7",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__F3DBC57208F9FBAD",
                table: "User",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
