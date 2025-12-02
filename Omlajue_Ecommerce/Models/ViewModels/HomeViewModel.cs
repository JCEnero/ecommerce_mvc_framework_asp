using System.Collections.Generic;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for the home page
    /// </summary>
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Product> MensWatches { get; set; }
        public List<Product> WomensWatches { get; set; }
        public List<Product> LuxuryWatches { get; set; }
        public List<Product> SportsWatches { get; set; }
        public bool IsLoggedIn { get; set; }
        
        // Filtering properties
        public int? SelectedBrandId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SelectedGender { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchQuery { get; set; }
        public string SortBy { get; set; }
    }
}
