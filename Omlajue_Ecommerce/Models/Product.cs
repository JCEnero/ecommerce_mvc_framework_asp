using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents a watch product
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string SKU { get; set; }

        [Required]
        public string Description { get; set; }

        public string Specifications { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public int LowStockThreshold { get; set; }

        // Watch-specific properties
        [StringLength(100)]
        public string CaseMaterial { get; set; }

        [StringLength(100)]
        public string BandMaterial { get; set; }

        [StringLength(100)]
        public string MovementType { get; set; }

        [StringLength(50)]
        public string WaterResistance { get; set; }

        [StringLength(50)]
        public string CaseDiameter { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
