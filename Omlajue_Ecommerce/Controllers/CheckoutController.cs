using Omlajue_Ecommerce.Helpers;
using Omlajue_Ecommerce.Models;
using Omlajue_Ecommerce.Models.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Controllers
{
    /// <summary>
    /// Controller for checkout and order processing
    /// </summary>
    [CustomAuthorize]
    public class CheckoutController : Controller
    {
        private readonly OmlajueDbContext _context;

        public CheckoutController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var userId = (int)Session["UserId"];
            
            // Check if cart has items
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Product.Brand)
                .Include(c => c.Product.ProductImages)
                .Where(c => c.UserId == userId)
                .ToList();
                
            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Cart");
            }

            // Get user info
            var user = _context.Users.Find(userId);

            // Prepare cart view model
            var cartViewModel = new CartViewModel();
            foreach (var item in cartItems)
            {
                var product = item.Product;
                var price = product.DiscountPrice ?? product.Price;
                
                cartViewModel.CartItems.Add(new CartItemViewModel
                {
                    CartItemId = item.CartItemId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    BrandName = product.Brand.BrandName,
                    Price = price,
                    Quantity = item.Quantity,
                    TotalPrice = price * item.Quantity,
                    ImageUrl = product.ProductImages.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? "/Content/images/placeholder.jpg"
                });
            }
            cartViewModel.CalculateTotals();

            var model = new CheckoutViewModel
            {
                BillingFirstName = user.FirstName,
                BillingLastName = user.LastName,
                BillingEmail = user.Email,
                BillingPhone = user.PhoneNumber,
                BillingAddress = user.Address,
                BillingCity = user.City,
                BillingProvince = user.Province,
                BillingPostalCode = user.PostalCode,
                Cart = cartViewModel
            };

            return View(model);
        }

        // POST: Checkout/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var userId = (int)Session["UserId"];
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Cart");
            }

            // Check stock availability
            foreach (var item in cartItems)
            {
                if (item.Product.StockQuantity < item.Quantity)
                {
                    TempData["Error"] = $"{item.Product.ProductName} is out of stock";
                    return RedirectToAction("Index");
                }
            }

            // Calculate totals
            var subTotal = cartItems.Sum(c => (c.Product.DiscountPrice ?? c.Product.Price) * c.Quantity);
            var shippingFee = 150m;
            var totalAmount = subTotal + shippingFee;

            // Create order
            var order = new Order
            {
                OrderNumber = OrderHelper.GenerateOrderNumber(),
                UserId = userId,
                OrderDate = DateTime.Now,
                SubTotal = subTotal,
                ShippingFee = shippingFee,
                TotalAmount = totalAmount,
                OrderStatus = "Pending",
                
                BillingFirstName = model.BillingFirstName,
                BillingLastName = model.BillingLastName,
                BillingEmail = model.BillingEmail,
                BillingPhone = model.BillingPhone,
                BillingAddress = model.BillingAddress,
                BillingCity = model.BillingCity,
                BillingProvince = model.BillingProvince,
                BillingPostalCode = model.BillingPostalCode,
                
                ShippingFirstName = model.ShipToDifferentAddress ? model.ShippingFirstName : model.BillingFirstName,
                ShippingLastName = model.ShipToDifferentAddress ? model.ShippingLastName : model.BillingLastName,
                ShippingPhone = model.ShipToDifferentAddress ? model.ShippingPhone : model.BillingPhone,
                ShippingAddress = model.ShipToDifferentAddress ? model.ShippingAddress : model.BillingAddress,
                ShippingCity = model.ShipToDifferentAddress ? model.ShippingCity : model.BillingCity,
                ShippingProvince = model.ShipToDifferentAddress ? model.ShippingProvince : model.BillingProvince,
                ShippingPostalCode = model.ShipToDifferentAddress ? model.ShippingPostalCode : model.BillingPostalCode,
                
                OrderNotes = model.OrderNotes
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Create order items
            foreach (var item in cartItems)
            {
                var product = item.Product;
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.DiscountPrice ?? product.Price,
                    Quantity = item.Quantity,
                    TotalPrice = (product.DiscountPrice ?? product.Price) * item.Quantity
                };
                _context.OrderItems.Add(orderItem);

                // Update stock
                product.StockQuantity -= item.Quantity;
            }

            // Create payment record
            var payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = model.PaymentMethod == "CashOnDelivery" ? "Pending" : "Pending",
                Amount = totalAmount,
                CreatedAt = DateTime.Now
            };

            // Generate payment number for GCash/Maya
            if (model.PaymentMethod == "GCash" || model.PaymentMethod == "Maya")
            {
                payment.AssignedPaymentNumber = OrderHelper.GeneratePaymentNumber();
            }

            _context.Payments.Add(payment);

            // Clear cart
            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();

            // Redirect based on payment method
            if (model.PaymentMethod == "GCash" || model.PaymentMethod == "Maya")
            {
                return RedirectToAction("Payment", new { orderId = order.OrderId });
            }
            else if (model.PaymentMethod == "DebitCard")
            {
                return RedirectToAction("DebitCardPayment", new { orderId = order.OrderId });
            }
            else
            {
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }
        }

        // GET: Checkout/Payment
        public ActionResult Payment(int orderId)
        {
            var userId = (int)Session["UserId"];
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Get payment separately
            ViewBag.Payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);

            return View(order);
        }

        // POST: Checkout/SubmitPaymentProof
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPaymentProof(PaymentProofViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", model);
            }

            var userId = (int)Session["UserId"];
            var order = _context.Orders
                .FirstOrDefault(o => o.OrderId == model.OrderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Get payment separately
            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == model.OrderId);

            // Save uploaded file
            if (model.ProofOfPayment != null && model.ProofOfPayment.ContentLength > 0)
            {
                var fileName = $"{order.OrderNumber}_{DateTime.Now.Ticks}{Path.GetExtension(model.ProofOfPayment.FileName)}";
                var path = Path.Combine(Server.MapPath("~/Uploads/PaymentProofs"), fileName);
                
                // Create directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                
                model.ProofOfPayment.SaveAs(path);
                payment.ProofOfPaymentUrl = $"/Uploads/PaymentProofs/{fileName}";
            }

            payment.ReferenceNumber = model.ReferenceNumber;
            payment.PaymentDate = model.PaymentDateTime;
            payment.ProofUploadedAt = DateTime.Now;
            payment.PaymentStatus = "Pending"; // Admin will approve

            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        // GET: Checkout/DebitCardPayment
        public ActionResult DebitCardPayment(int orderId)
        {
            var userId = (int)Session["UserId"];
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Get payment separately
            ViewBag.Payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);

            return View(order);
        }

        // POST: Checkout/ProcessDebitCard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessDebitCard(int orderId, string cardNumber, string cardHolder)
        {
            var userId = (int)Session["UserId"];
            var order = _context.Orders
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Get payment separately
            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);
            
            // Store only last 4 digits for security
            payment.CardLastFourDigits = cardNumber.Substring(cardNumber.Length - 4);
            payment.CardHolderName = cardHolder;
            payment.PaymentStatus = "Completed";
            payment.PaymentDate = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;

            order.OrderStatus = "Processing";

            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        // GET: Checkout/OrderConfirmation
        public ActionResult OrderConfirmation(int orderId)
        {
            var userId = (int)Session["UserId"];
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Get payment separately
            ViewBag.Payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);

            return View(order);
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
