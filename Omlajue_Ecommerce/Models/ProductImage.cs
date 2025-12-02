using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents product images (gallery for each watch)
    /// </summary>
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool IsPrimary { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
