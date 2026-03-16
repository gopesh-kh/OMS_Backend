using Microsoft.EntityFrameworkCore;
using OMS_Backend.Models;

namespace OMS_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder);
            ConfigureAddress(modelBuilder);
            ConfigureCart(modelBuilder);
            ConfigureCartItem(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureProduct(modelBuilder);
            ConfigureFavourite(modelBuilder);
            ConfigureProductReview(modelBuilder);
            ConfigureOrder(modelBuilder);
            ConfigureOrderItem(modelBuilder);

            //seedData(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasIndex(a => a.UserId);
        }

        private void ConfigureCart(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.UserId)
                .IsUnique();
        }

        private void ConfigureCartItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId })
                .IsUnique();
        }

        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();
        }

        private void ConfigureProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));
        }

        private void ConfigureFavourite(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Product)
                .WithMany()
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favourite>()
                .HasIndex(f => new { f.UserId, f.ProductId })
                .IsUnique();
        }

        private void ConfigureProductReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.User)
                .WithMany(u => u.ProductReviews)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany()
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductReview>()
                .HasIndex(pr => new { pr.UserId, pr.ProductId })
                .IsUnique();
        }

        private void ConfigureOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.ShippingAddressId);
        }

        private void ConfigureOrderItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => new { oi.OrderId, oi.ProductId })
                .IsUnique();
        }

        //public override async Task<int> SaveChangesAsync(
        //    CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries<BaseEntity>();

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedAt = DateTime.UtcNow;
        //            entry.Entity.CreatedBy = 1;
        //        }
        //        else if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.ModifiedAt = DateTime.UtcNow;
        //            entry.Entity.ModifiedBy = 1;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        private void seedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" },
                new UserRole { Id = 2, RoleName = "Vendor" },
                new UserRole { Id = 3, RoleName = "Customer" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Clothing" },
                new Category { CategoryId = 3, CategoryName = "Home & Kitchen" },
                new Category { CategoryId = 4, CategoryName = "Books" },
                new Category { CategoryId = 5, CategoryName = "Beauty & Personal Care" },
                new Category { CategoryId = 6, CategoryName = "Sports & Fitness" },
                new Category { CategoryId = 7, CategoryName = "Toys & Games" },
                new Category { CategoryId = 8, CategoryName = "Automotive" },
                new Category { CategoryId = 9, CategoryName = "Groceries" },
                new Category { CategoryId = 10, CategoryName = "Furniture" }
            );
        }
    }
}