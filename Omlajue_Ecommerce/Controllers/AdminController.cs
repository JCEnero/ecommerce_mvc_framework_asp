using Omlajue_Ecommerce.Helpers;
using Omlajue_Ecommerce.Models;
using Omlajue_Ecommerce.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for admin panel operations
    /// </summary>
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly OmlajueDbContext _context;

        public AdminController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Admin/Index (Dashboard)
        public ActionResult Index()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var model = new AdminDashboardViewModel
            {
                TotalOrdersToday = _context.Orders.Count(o => o.OrderDate >= today),
                TotalPendingOrders = _context.Orders.Count(o => o.OrderStatus == "Pending"),
                TotalSalesToday = _context.Orders
                    .Where(o => o.OrderDate >= today && o.OrderStatus != "Cancelled")
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0,
                TotalSalesThisMonth = _context.Orders
                    .Where(o => o.OrderDate >= startOfMonth && o.OrderStatus != "Cancelled")
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0,
                TotalProducts = _context.Products.Count(p => p.IsActive),
                LowStockProducts = _context.Products.Count(p => p.IsActive && p.StockQuantity <= p.LowStockThreshold),
                TotalCustomers = _context.Users.Count(u => !u.IsAdmin),
                NewCustomersThisMonth = _context.Users.Count(u => !u.IsAdmin && u.CreatedAt >= startOfMonth),
                PendingPayments = _context.Payments.Count(p => p.PaymentStatus == "Pending")
            };

            return View(model);
        }

        #region Product Management

        // GET: Admin/Products
        public ActionResult Products()
        {
            var products = _context.Products.OrderByDescending(p => p.CreatedAt).ToList();
            return View(products);
        }

        // GET: Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_context.Categories.Where(c => c.IsActive), "CategoryId", "CategoryName");
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "BrandId", "BrandName");
            return View();
        }

        // POST: Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase[] images)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.Now;
                product.IsActive = true;
                
                _context.Products.Add(product);
                _context.SaveChanges();

                // Handle image uploads
                if (images != null && images.Length > 0)
                {
                    var uploadPath = Server.MapPath("~/Uploads/Products");
                    Directory.CreateDirectory(uploadPath);

                    for (int i = 0; i < images.Length; i++)
                    {
                        var image = images[i];
                        if (image != null && image.ContentLength > 0)
                        {
                            var fileName = $"{product.ProductId}_{DateTime.Now.Ticks}_{i}{Path.GetExtension(image.FileName)}";
                            var path = Path.Combine(uploadPath, fileName);
                            image.SaveAs(path);

                            var productImage = new ProductImage
                            {
                                ProductId = product.ProductId,
                                ImageUrl = $"/Uploads/Products/{fileName}",
                                IsPrimary = i == 0,
                                DisplayOrder = i
                            };
                            _context.ProductImages.Add(productImage);
                        }
                    }
                    _context.SaveChanges();
                }

                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Products");
            }

            ViewBag.Categories = new SelectList(_context.Categories.Where(c => c.IsActive), "CategoryId", "CategoryName");
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "BrandId", "BrandName");
            return View(product);
        }

        // GET: Admin/EditProduct/5
        public ActionResult EditProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories.Where(c => c.IsActive), "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "BrandId", "BrandName", product.BrandId);
            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product, HttpPostedFileBase[] images)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.SKU = product.SKU;
                existingProduct.Description = product.Description;
                existingProduct.Specifications = product.Specifications;
                existingProduct.Price = product.Price;
                existingProduct.DiscountPrice = product.DiscountPrice;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.LowStockThreshold = product.LowStockThreshold;
                existingProduct.CaseMaterial = product.CaseMaterial;
                existingProduct.BandMaterial = product.BandMaterial;
                existingProduct.MovementType = product.MovementType;
                existingProduct.WaterResistance = product.WaterResistance;
                existingProduct.CaseDiameter = product.CaseDiameter;
                existingProduct.Gender = product.Gender;
                existingProduct.IsFeatured = product.IsFeatured;
                existingProduct.IsActive = product.IsActive;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.BrandId = product.BrandId;
                existingProduct.UpdatedAt = DateTime.Now;

                // Handle new image uploads
                if (images != null && images.Length > 0)
                {
                    var uploadPath = Server.MapPath("~/Uploads/Products");
                    Directory.CreateDirectory(uploadPath);

                    var existingImagesCount = existingProduct.ProductImages.Count;

                    for (int i = 0; i < images.Length; i++)
                    {
                        var image = images[i];
                        if (image != null && image.ContentLength > 0)
                        {
                            var fileName = $"{product.ProductId}_{DateTime.Now.Ticks}_{i}{Path.GetExtension(image.FileName)}";
                            var path = Path.Combine(uploadPath, fileName);
                            image.SaveAs(path);

                            var productImage = new ProductImage
                            {
                                ProductId = product.ProductId,
                                ImageUrl = $"/Uploads/Products/{fileName}",
                                IsPrimary = existingImagesCount == 0 && i == 0,
                                DisplayOrder = existingImagesCount + i
                            };
                            _context.ProductImages.Add(productImage);
                        }
                    }
                }

                _context.SaveChanges();

                TempData["Success"] = "Product updated successfully";
                return RedirectToAction("Products");
            }

            ViewBag.Categories = new SelectList(_context.Categories.Where(c => c.IsActive), "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "BrandId", "BrandName", product.BrandId);
            return View(product);
        }

        // POST: Admin/DeleteProduct/5
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.IsActive = false;
                _context.SaveChanges();
                return Json(new { success = true, message = "Product deleted successfully" });
            }
            return Json(new { success = false, message = "Product not found" });
        }

        #endregion

        #region Order Management

        // GET: Admin/Orders
        public ActionResult Orders()
        {
            var orders = _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }

        // GET: Admin/OrderDetails/5
        public ActionResult OrderDetails(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/UpdateOrderStatus
        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }

            order.OrderStatus = status;
            
            if (status == "Completed")
            {
                order.CompletedAt = DateTime.Now;
            }
            else if (status == "Cancelled")
            {
                order.CancelledAt = DateTime.Now;
                
                // Restore stock
                foreach (var item in order.OrderItems)
                {
                    var product = _context.Products.Find(item.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                    }
                }
            }

            _context.SaveChanges();
            return Json(new { success = true, message = "Order status updated" });
        }

        #endregion

        #region Payment Management

        // GET: Admin/Payments
        public ActionResult Payments()
        {
            var payments = _context.Payments
                .Where(p => p.PaymentMethod == "GCash" || p.PaymentMethod == "Maya")
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            return View(payments);
        }

        // POST: Admin/ApprovePayment
        [HttpPost]
        public ActionResult ApprovePayment(int paymentId)
        {
            var payment = _context.Payments.Find(paymentId);
            if (payment == null)
            {
                return Json(new { success = false, message = "Payment not found" });
            }

            payment.PaymentStatus = "Approved";
            payment.UpdatedAt = DateTime.Now;
            payment.Order.OrderStatus = "Processing";

            _context.SaveChanges();
            return Json(new { success = true, message = "Payment approved" });
        }

        // POST: Admin/RejectPayment
        [HttpPost]
        public ActionResult RejectPayment(int paymentId, string reason)
        {
            var payment = _context.Payments.Find(paymentId);
            if (payment == null)
            {
                return Json(new { success = false, message = "Payment not found" });
            }

            payment.PaymentStatus = "Rejected";
            payment.PaymentNotes = reason;
            payment.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return Json(new { success = true, message = "Payment rejected" });
        }

        #endregion

        #region Category Management

        // GET: Admin/Categories
        public ActionResult Categories()
        {
            var categories = _context.Categories.OrderBy(c => c.DisplayOrder).ToList();
            return View(categories);
        }

        // POST: Admin/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.IsActive = true;
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category created successfully";
            }
            return RedirectToAction("Categories");
        }

        // POST: Admin/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Categories.Find(category.CategoryId);
                if (existing != null)
                {
                    existing.CategoryName = category.CategoryName;
                    existing.Description = category.Description;
                    existing.DisplayOrder = category.DisplayOrder;
                    existing.IsActive = category.IsActive;
                    _context.SaveChanges();
                    TempData["Success"] = "Category updated successfully";
                }
            }
            return RedirectToAction("Categories");
        }

        #endregion

        #region Brand Management

        // GET: Admin/Brands
        public ActionResult Brands()
        {
            var brands = _context.Brands.OrderBy(b => b.BrandName).ToList();
            return View(brands);
        }

        // POST: Admin/CreateBrand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.IsActive = true;
                _context.Brands.Add(brand);
                _context.SaveChanges();
                TempData["Success"] = "Brand created successfully";
            }
            return RedirectToAction("Brands");
        }

        // POST: Admin/EditBrand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Brands.Find(brand.BrandId);
                if (existing != null)
                {
                    existing.BrandName = brand.BrandName;
                    existing.Description = brand.Description;
                    existing.IsActive = brand.IsActive;
                    _context.SaveChanges();
                    TempData["Success"] = "Brand updated successfully";
                }
            }
            return RedirectToAction("Brands");
        }

        #endregion

        #region User Management

        // GET: Admin/Users
        public ActionResult Users()
        {
            var users = _context.Users.Where(u => !u.IsAdmin).OrderByDescending(u => u.CreatedAt).ToList();
            return View(users);
        }

        // GET: Admin/UserDetails/5
        public ActionResult UserDetails(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        #endregion

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
