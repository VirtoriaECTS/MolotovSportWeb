using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MolotovSportWeb.Models;

public partial class MolotovSportWebContext : DbContext
{
    public MolotovSportWebContext()
    {
    }

    public MolotovSportWebContext(DbContextOptions<MolotovSportWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriesMini> CategoriesMinis { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Firm> Firms { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShopingCart> ShopingCarts { get; set; }

    public virtual DbSet<ShopingItem> ShopingItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LISAMOLOTOVA;Database=MolotovSportWeb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriesMini>(entity =>
        {
            entity.HasKey(e => new { e.CategoryMiniId, e.CategoryId });

            entity.ToTable("CategoriesMini");

            entity.Property(e => e.CategoryMiniId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryMiniName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.CategoriesMinis)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriesMini_Categories");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Firm>(entity =>
        {
            entity.Property(e => e.FirmName).HasMaxLength(50);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.GenderName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemsId);

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_Product");

            entity.HasOne(d => d.Size).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_ProductSize");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Firm).WithMany(p => p.Products)
                .HasForeignKey(d => d.FirmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Firms");

            entity.HasOne(d => d.Gender).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Gender");

            entity.HasOne(d => d.CategoriesMini).WithMany(p => p.Products)
                .HasForeignKey(d => new { d.CategoryIdMini, d.CategoryId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_CategoriesMini");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.SizeId);

            entity.ToTable("ProductSize");

            entity.Property(e => e.Size).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductSize_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<ShopingCart>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.ToTable("ShopingCart");

            entity.Property(e => e.TotalAmout).HasColumnType("money");

            entity.HasOne(d => d.User).WithMany(p => p.ShopingCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopingCart_User");
        });

        modelBuilder.Entity<ShopingItem>(entity =>
        {
            entity.HasKey(e => e.CardItemId);

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Cart).WithMany(p => p.ShopingItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopingItems_ShopingCart");

            entity.HasOne(d => d.Product).WithMany(p => p.ShopingItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopingItems_Product");

            entity.HasOne(d => d.Size).WithMany(p => p.ShopingItems)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopingItems_ProductSize");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
