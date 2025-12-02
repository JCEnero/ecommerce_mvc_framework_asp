using Omlajue_Ecommerce.Helpers;
using Omlajue_Ecommerce.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for wishlist operations
    /// </summary>
    [CustomAuthorize]
    public class WishlistController : Controller
    {
        private readonly OmlajueDbContext _context;

        public WishlistController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Wishlist
        public ActionResult Index()
        {
            var userId = (int)Session["UserId"];
            var wishlistItems = _context.WishlistItems
                .Include(w => w.Product)
                .Include(w => w.Product.Brand)
                .Include(w => w.Product.Category)
                .Include(w => w.Product.ProductImages)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.AddedAt)
                .ToList();

            return View(wishlistItems);
        }

        // POST: Wishlist/Add
        [HttpPost]
        public ActionResult Add(int productId)
        {
            var userId = (int)Session["UserId"];

            // Check if product exists
            var product = _context.Products.Find(productId);
            if (product == null || !product.IsActive)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            // Check if already in wishlist
            var exists = _context.WishlistItems
                .Any(w => w.UserId == userId && w.ProductId == productId);

            if (exists)
            {
                return Json(new { success = false, message = "Product already in wishlist" });
            }

            // Add to wishlist
            var wishlistItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.Now
            };

            _context.WishlistItems.Add(wishlistItem);
            _context.SaveChanges();

            return Json(new { success = true, message = "Product added to wishlist" });
        }

        // POST: Wishlist/Remove
        [HttpPost]
        public ActionResult Remove(int wishlistItemId)
        {
            var userId = (int)Session["UserId"];
            var wishlistItem = _context.WishlistItems
                .FirstOrDefault(w => w.WishlistItemId == wishlistItemId && w.UserId == userId);

            if (wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
                _context.SaveChanges();
            }

            return Json(new { success = true, message = "Item removed from wishlist" });
        }

        // POST: Wishlist/MoveToCart
        [HttpPost]
        public ActionResult MoveToCart(int wishlistItemId)
        {
            var userId = (int)Session["UserId"];
            var wishlistItem = _context.WishlistItems
                .Include(w => w.Product)
                .FirstOrDefault(w => w.WishlistItemId == wishlistItemId && w.UserId == userId);

            if (wishlistItem == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }

            // Check if product is in stock
            if (wishlistItem.Product.StockQuantity < 1)
            {
                return Json(new { success = false, message = "Product out of stock" });
            }

            // Check if already in cart
            var existingCartItem = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == wishlistItem.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = wishlistItem.ProductId,
                    Quantity = 1,
                    AddedAt = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            // Remove from wishlist
            _context.WishlistItems.Remove(wishlistItem);
            _context.SaveChanges();

            return Json(new { success = true, message = "Item moved to cart" });
        }

        // GET: Wishlist/Count
        public ActionResult Count()
        {
            if (Session["UserId"] == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var userId = (int)Session["UserId"];
            var count = _context.WishlistItems.Count(w => w.UserId == userId);

            return Json(new { count }, JsonRequestBehavior.AllowGet);
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
