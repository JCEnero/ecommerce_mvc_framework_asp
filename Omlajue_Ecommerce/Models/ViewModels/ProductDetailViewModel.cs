namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for product details page
    /// </summary>
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public bool IsInWishlist { get; set; }
        public bool IsInCart { get; set; }
    }
}
