using Omlajue_Ecommerce.Models;
using Omlajue_Ecommerce.Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for home page and public pages
    /// </summary>
    public class HomeController : Controller
    {
        private readonly OmlajueDbContext _context;

        public HomeController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Home
        public ActionResult Index(int? brandId = null, int? categoryId = null, string gender = null, 
                                  decimal? minPrice = null, decimal? maxPrice = null, string search = null, string sortBy = "newest")
        {
            var isLoggedIn = Session["UserId"] != null;
            
            var model = new HomeViewModel
            {
                IsLoggedIn = isLoggedIn,
                SelectedBrandId = brandId,
                SelectedCategoryId = categoryId,
                SelectedGender = gender,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SearchQuery = search,
                SortBy = sortBy,
                
                Brands = _context.Brands
                    .Where(b => b.IsActive)
                    .OrderBy(b => b.BrandName)
                    .ToList(),
                
                Categories = _context.Categories
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.DisplayOrder)
                    .ToList()
            };

            if (isLoggedIn)
            {
                // Load all products with filtering for logged-in users
                var productsQuery = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive);

                // Apply filters
                if (brandId.HasValue)
                    productsQuery = productsQuery.Where(p => p.BrandId == brandId.Value);

                if (categoryId.HasValue)
                    productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);

                if (!string.IsNullOrEmpty(gender))
                    productsQuery = productsQuery.Where(p => p.Gender == gender);

                if (minPrice.HasValue)
                    productsQuery = productsQuery.Where(p => (p.DiscountPrice ?? p.Price) >= minPrice.Value);

                if (maxPrice.HasValue)
                    productsQuery = productsQuery.Where(p => (p.DiscountPrice ?? p.Price) <= maxPrice.Value);

                if (!string.IsNullOrEmpty(search))
                    productsQuery = productsQuery.Where(p => 
                        p.ProductName.Contains(search) || 
                        p.Description.Contains(search) ||
                        p.Brand.BrandName.Contains(search));

                // Apply sorting
                switch (sortBy?.ToLower())
                {
                    case "price-asc":
                        productsQuery = productsQuery.OrderBy(p => p.DiscountPrice ?? p.Price);
                        break;
                    case "price-desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.DiscountPrice ?? p.Price);
                        break;
                    case "name":
                        productsQuery = productsQuery.OrderBy(p => p.ProductName);
                        break;
                    case "newest":
                    default:
                        productsQuery = productsQuery.OrderByDescending(p => p.CreatedAt);
                        break;
                }

                model.AllProducts = productsQuery.ToList();
            }
            else
            {
                // Load featured products for guest users
                model.FeaturedProducts = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive && p.IsFeatured)
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(10)
                    .ToList();
                
                model.MensWatches = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive && p.Gender == "Men")
                    .Take(4)
                    .ToList();
                
                model.WomensWatches = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive && p.Gender == "Women")
                    .Take(4)
                    .ToList();
                
                model.LuxuryWatches = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive && p.Price >= 50000)
                    .Take(4)
                    .ToList();
                
                model.SportsWatches = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.IsActive && p.Category.CategoryName.Contains("Sports"))
                    .Take(4)
                    .ToList();
            }

            return View(model);
        }

        // GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: Home/TermsAndConditions
        public ActionResult TermsAndConditions()
        {
            return View();
        }

        // GET: Home/PrivacyPolicy
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}