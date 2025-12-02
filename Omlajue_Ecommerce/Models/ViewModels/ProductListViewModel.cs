using System.Collections.Generic;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for product listing page with filters
    /// </summary>
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }

        // Filter parameters
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Gender { get; set; }
        public string SortBy { get; set; }
        public string SearchTerm { get; set; }

        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
