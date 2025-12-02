using Omlajue_Ecommerce.Helpers;
using Omlajue_Ecommerce.Models;
using Omlajue_Ecommerce.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for shopping cart operations
    /// </summary>
    [CustomAuthorize]
    public class CartController : Controller
    {
        private readonly OmlajueDbContext _context;

        public CartController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var userId = (int)Session["UserId"];
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Product.Brand)
                .Include(c => c.Product.ProductImages)
                .Where(c => c.UserId == userId)
                .ToList();

            var model = new CartViewModel();

            foreach (var item in cartItems)
            {
                var product = item.Product;
                var primaryImage = product.ProductImages.FirstOrDefault(i => i.IsPrimary)?.ImageUrl 
                    ?? "/Content/images/placeholder.jpg";

                model.CartItems.Add(new CartItemViewModel
                {
                    CartItemId = item.CartItemId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    BrandName = product.Brand.BrandName,
                    Price = product.DiscountPrice ?? product.Price,
                    Quantity = item.Quantity,
                    TotalPrice = (product.DiscountPrice ?? product.Price) * item.Quantity,
                    ImageUrl = primaryImage,
                    StockQuantity = product.StockQuantity
                });
            }

            model.CalculateTotals();

            return View(model);
        }

        // POST: Cart/Add
        [HttpPost]
        public ActionResult Add(int productId, int quantity = 1)
        {
            var userId = (int)Session["UserId"];

            // Check if product exists and is active
            var product = _context.Products.Find(productId);
            if (product == null || !product.IsActive)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            // Check stock
            if (product.StockQuantity < quantity)
            {
                return Json(new { success = false, message = "Insufficient stock" });
            }

            // Check if already in cart
            var existingItem = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (existingItem != null)
            {
                // Update quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new item
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    AddedAt = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();

            return Json(new { success = true, message = "Product added to cart" });
        }

        // POST: Cart/Update
        [HttpPost]
        public ActionResult Update(int cartItemId, int quantity)
        {
            var userId = (int)Session["UserId"];
            var cartItem = _context.CartItems
                .Include(c => c.Product)
                .FirstOrDefault(c => c.CartItemId == cartItemId && c.UserId == userId);

            if (cartItem == null)
            {
                return Json(new { success = false, message = "Cart item not found" });
            }

            // Check stock
            if (cartItem.Product.StockQuantity < quantity)
            {
                return Json(new { success = false, message = "Insufficient stock" });
            }

            cartItem.Quantity = quantity;
            _context.SaveChanges();

            return Json(new { success = true, message = "Cart updated" });
        }

        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int cartItemId)
        {
            var userId = (int)Session["UserId"];
            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.CartItemId == cartItemId && c.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return Json(new { success = true, message = "Item removed from cart" });
        }

        // POST: Cart/Clear
        [HttpPost]
        public ActionResult Clear()
        {
            var userId = (int)Session["UserId"];
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return Json(new { success = true, message = "Cart cleared" });
        }

        // GET: Cart/Count
        public ActionResult Count()
        {
            if (Session["UserId"] == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var userId = (int)Session["UserId"];
            var count = _context.CartItems.Count(c => c.UserId == userId);

            return Json(new { count }, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart/GetCartCount (alias for Count to match JavaScript call)
        public ActionResult GetCartCount()
        {
            if (Session["UserId"] == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var userId = (int)Session["UserId"];
            var count = _context.CartItems.Count(c => c.UserId == userId);

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
