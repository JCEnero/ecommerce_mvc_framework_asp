using Omlajue_Ecommerce.Models;
using Omlajue_Ecommerce.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for product browsing and details
    /// </summary>
    public class ProductController : Controller
    {
        private readonly OmlajueDbContext _context;

        public ProductController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Product/List
        public ActionResult List(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, 
            string gender, string sortBy, string searchTerm, int page = 1, int pageSize = 12)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Where(p => p.IsActive);

            // Apply filters
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(p => p.BrandId == brandId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(p => p.Gender == gender);

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p => p.ProductName.Contains(searchTerm) || 
                    p.Description.Contains(searchTerm) ||
                    p.Brand.BrandName.Contains(searchTerm));

            // Apply sorting
            switch (sortBy)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.ProductName);
                    break;
                case "newest":
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(p => p.ProductName);
                    break;
            }

            // Get total count for pagination
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Apply pagination
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new ProductListViewModel
            {
                Products = products,
                Brands = _context.Brands.Where(b => b.IsActive).ToList(),
                Categories = _context.Categories.Where(c => c.IsActive).ToList(),
                CategoryId = categoryId,
                BrandId = brandId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Gender = gender,
                SortBy = sortBy,
                SearchTerm = searchTerm,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return View(model);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.ProductId == id && p.IsActive);

            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductDetailViewModel
            {
                Product = product,
                IsInWishlist = false,
                IsInCart = false
            };

            // Check if user is logged in and if product is in wishlist/cart
            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                model.IsInWishlist = _context.WishlistItems.Any(w => w.UserId == userId && w.ProductId == id);
                model.IsInCart = _context.CartItems.Any(c => c.UserId == userId && c.ProductId == id);
            }

            return View(model);
        }

        // GET: Product/Search
        public ActionResult Search(string q)
        {
            return RedirectToAction("List", new { searchTerm = q });
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
