using System.Data.Entity;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Database context for Omlajue Ecommerce application
    /// </summary>
    public class OmlajueDbContext : DbContext
    {
        public OmlajueDbContext() : base("name=OmlajueConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // NOTE: For MySQL with EF6, we don't use HasPrecision() 
            // Instead, use [Column(TypeName = "decimal(18,2)")] attribute on the model properties
            // The precision is defined in the model classes, not here in Fluent API

            // ===== Configure all relationships explicitly =====
            
            // User-Order relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithRequired(o => o.User)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            // User-CartItem relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CartItems)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);

            // User-WishlistItem relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.WishlistItems)
                .WithRequired(w => w.User)
                .HasForeignKey(w => w.UserId)
                .WillCascadeOnDelete(true);

            // Category-Product relationship (one-to-many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            // Brand-Product relationship (one-to-many)
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Products)
                .WithRequired(p => p.Brand)
                .HasForeignKey(p => p.BrandId)
                .WillCascadeOnDelete(false);

            // Product-ProductImage relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithRequired(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .WillCascadeOnDelete(true);

            // Product-CartItem relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithRequired(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .WillCascadeOnDelete(false);

            // Product-WishlistItem relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.WishlistItems)
                .WithRequired(w => w.Product)
                .HasForeignKey(w => w.ProductId)
                .WillCascadeOnDelete(false);

            // Product-OrderItem relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithRequired(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .WillCascadeOnDelete(false);

            // Order-OrderItem relationship (one-to-many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithRequired(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete(true);

            // Order-Payment relationship
            // Since Order no longer has a Payment navigation property,
            // this is now a simple one-directional many-to-one relationship
            // Payment -> Order (many payments could theoretically point to one order,
            // but the unique index enforces one-to-one at database level)
            modelBuilder.Entity<Payment>()
                .HasRequired(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .WillCascadeOnDelete(false);

            // ===== Configure unique constraints =====
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Make OrderId in Payment unique to enforce one payment per order at database level
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.OrderId)
                .IsUnique();
        }
    }
}
