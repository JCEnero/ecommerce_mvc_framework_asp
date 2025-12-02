using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents a product category (e.g., Men's Watch, Women's Watch, Luxury Watch, Sports Watch)
    /// </summary>
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
