using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebDienThoai.Models;

public partial class DatabaseTheKingContext : DbContext
{
    public DatabaseTheKingContext()
    {
    }

    public DatabaseTheKingContext(DbContextOptions<DatabaseTheKingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HoangHung;Initial Catalog=DatabaseTheKing;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83FE65758FB");
            entity.ToTable("Category");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
            entity.Property(e => e.Slug).HasMaxLength(255).IsUnicode(false).HasColumnName("slug");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Inventor__B40CC6CD941DC666");
            entity.ToTable("Inventory");
            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.QuantityInStock).HasDefaultValue(0);
            entity.HasOne(d => d.Product).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Produ__440B1D61");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83F67510456");
            entity.ToTable("Order");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Orderdate).HasDefaultValueSql("(getdate())").HasColumnType("datetime").HasColumnName("orderdate");
            entity.Property(e => e.Paymentstatus).HasMaxLength(50).HasDefaultValue("Chưa thanh toán").HasColumnName("paymentstatus");
            entity.Property(e => e.Shipaddress).HasColumnName("shipaddress");
            entity.Property(e => e.Shipname).HasMaxLength(255).HasColumnName("shipname");
            entity.Property(e => e.Shipphone).HasMaxLength(20).IsUnicode(false).HasColumnName("shipphone");
            entity.Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Chờ xử lý").HasColumnName("status");
            entity.Property(e => e.Totalprice).HasColumnType("decimal(15, 2)").HasColumnName("totalprice");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__userid__49C3F6B7");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Productid }).HasName("PK__OrderIte__3ADF45A617256A6F");
            entity.ToTable("OrderItem");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unitprice).HasColumnType("decimal(15, 2)").HasColumnName("unitprice");
            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__order__4CA06362");
            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__produ__4D94879B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83FB57B5FD7");
            entity.ToTable("Product");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.ImageUrl).HasMaxLength(500).IsUnicode(false).HasColumnName("imageUrl");
            entity.Property(e => e.IsOnSale).HasDefaultValue(false).HasColumnName("isOnSale");
            entity.Property(e => e.IsPublished).HasDefaultValue(true).HasColumnName("isPublished");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnType("decimal(15, 2)").HasColumnName("price");
            entity.Property(e => e.Saleprice).HasColumnType("decimal(15, 2)").HasColumnName("saleprice");
            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__categor__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83FC1F34C1D");
            entity.ToTable("User");
            entity.HasIndex(e => e.Email, "UQ__User__AB6E616496D0E9A7").IsUnique();
            entity.HasIndex(e => e.Username, "UQ__User__F3DBC57208F9FBAD").IsUnique();
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Fullname).HasMaxLength(255).HasColumnName("fullname");
            entity.Property(e => e.Passwordhash).HasMaxLength(255).IsUnicode(false).HasColumnName("passwordhash");
            entity.Property(e => e.Role).HasMaxLength(50).IsUnicode(false).HasDefaultValue("Customer").HasColumnName("role");
            entity.Property(e => e.Username).HasMaxLength(50).IsUnicode(false).HasColumnName("username");
        });

       

        // 1. Thêm Người dùng
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Fullname = "Quản Trị Viên", Email = "admin@theking.vn", Passwordhash = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "khachhang", Fullname = "Nguyễn Văn A", Email = "khachhang@gmail.com", Passwordhash = "123456", Role = "Customer" },
            new User { Id = 3, Username = "khachhang2", Fullname = "Trần Thị B", Email = "tranthib@gmail.com", Passwordhash = "123456", Role = "Customer" }
        );

        // 2. Thêm Danh mục
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Apple", Slug = "apple" },
            new Category { Id = 2, Name = "Samsung", Slug = "samsung" },
            new Category { Id = 3, Name = "Xiaomi", Slug = "xiaomi" },
            new Category { Id = 4, Name = "Phụ Kiện", Slug = "phu-kien" },
            new Category { Id = 5, Name = "Tai Nghe", Slug = "tai-nghe" },
            new Category { Id = 6, Name = "Ốp Lưng", Slug = "op-lung" },
            new Category { Id = 7, Name = "Dây Sạc", Slug = "day-sac" }
        );

        // 3. Thêm Sản phẩm
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Categoryid = 1, Name = "iPhone 15 Pro", Price = 28990000, Saleprice = 27590000, IsOnSale = true, IsPublished = true, ImageUrl = "/img/10.jpg", Description = "Titanium, Chip A17 Pro mạnh mẽ." },
            new Product { Id = 2, Categoryid = 2, Name = "Samsung S25", Price = 27500000, Saleprice = 27500000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/20.jpg", Description = "Camera AI đột phá, màn hình Dynamic AMOLED 2X." },
            new Product { Id = 3, Categoryid = 1, Name = "Iphone 13", Price = 22990000, Saleprice = 22990000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/9.jpg", Description = "Thiết kế bền bỉ, Camera kép tiên tiến." },
            new Product { Id = 4, Categoryid = 1, Name = "iPhone 14", Price = 13790000, Saleprice = 12990000, IsOnSale = true, IsPublished = true, ImageUrl = "/img/19.jpg", Description = "Pin trâu, hiệu năng ổn định." },
            new Product { Id = 5, Categoryid = 1, Name = "iPhone 17", Price = 34990000, Saleprice = 34990000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/iphone-17.jpg", Description = "Công nghệ tương lai, thiết kế siêu mỏng." },
            new Product { Id = 6, Categoryid = 3, Name = "Xiaomi 14 Ultra", Price = 21990000, Saleprice = 21990000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/17.jpg", Description = "Hợp tác cùng Leica, nhiếp ảnh đỉnh cao." },
            new Product { Id = 7, Categoryid = 5, Name = "AirPods Pro 2", Price = 6190000, Saleprice = 5490000, IsOnSale = true, IsPublished = true, ImageUrl = "/img/14.jpg", Description = "Chống ồn chủ động, âm thanh không gian." },
            new Product { Id = 8, Categoryid = 6, Name = "Ốp lưng Spigen", Price = 750000, Saleprice = 750000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/22.jpg", Description = "Chống sốc chuẩn quân đội." },
            new Product { Id = 9, Categoryid = 7, Name = "Cáp Type C Anker", Price = 350000, Saleprice = 350000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/26.jpg", Description = "Siêu bền, hỗ trợ sạc nhanh 100W." },
            new Product { Id = 10, Categoryid = 6, Name = "Ốp lưng iPhone 15", Price = 450000, Saleprice = 450000, IsOnSale = false, IsPublished = true, ImageUrl = "/img/21.jpg", Description = "Trong suốt, không ố vàng." }
        );

        // 4. Thêm Tồn kho
        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { ProductId = 1, QuantityInStock = 50 },
            new Inventory { ProductId = 2, QuantityInStock = 20 },
            new Inventory { ProductId = 3, QuantityInStock = 15 },
            new Inventory { ProductId = 4, QuantityInStock = 100 },
            new Inventory { ProductId = 5, QuantityInStock = 5 },
            new Inventory { ProductId = 6, QuantityInStock = 30 },
            new Inventory { ProductId = 7, QuantityInStock = 200 },
            new Inventory { ProductId = 8, QuantityInStock = 500 },
            new Inventory { ProductId = 9, QuantityInStock = 300 },
            new Inventory { ProductId = 10, QuantityInStock = 150 }
        );

        // 5. Thêm Đơn hàng mẫu
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, Userid = 2, Orderdate = new DateTime(2023, 11, 20), Status = "Đang xử lý", Paymentstatus = "Chưa thanh toán", Totalprice = 33080000, Shipname = "Nguyễn Văn A", Shipaddress = "123 Lê Lợi, Q1, HCM", Shipphone = "0909123456" },
            new Order { Id = 2, Userid = 3, Orderdate = new DateTime(2023, 11, 18), Status = "Đã giao", Paymentstatus = "Đã thanh toán", Totalprice = 5490000, Shipname = "Trần Thị B", Shipaddress = "456 Nguyễn Huệ, Q1, HCM", Shipphone = "0912345678" }
        );

        // 6. Thêm Chi tiết đơn hàng
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Orderid = 1, Productid = 1, Quantity = 1, Unitprice = 27590000 },
            new OrderItem { Orderid = 1, Productid = 7, Quantity = 1, Unitprice = 5490000 },
            new OrderItem { Orderid = 2, Productid = 7, Quantity = 1, Unitprice = 5490000 }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}