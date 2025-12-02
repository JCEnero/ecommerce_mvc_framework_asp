using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents a watch brand (e.g., Rolex, Omega, Seiko)
    /// </summary>
    public class Brand
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string BrandName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
