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
    /// Controller for authentication and account management
    /// </summary>
    public class AccountController : Controller
    {
        private readonly OmlajueDbContext _context;

        public AccountController()
        {
            _context = new OmlajueDbContext();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered");
                    return View(model);
                }

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = PasswordHelper.HashPassword(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    IsAdmin = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.IsActive);

                if (user != null && PasswordHelper.VerifyPassword(model.Password, user.PasswordHash))
                {
                    // Set session
                    Session["UserId"] = user.UserId;
                    Session["UserName"] = $"{user.FirstName} {user.LastName}";
                    Session["IsAdmin"] = user.IsAdmin;

                    // Update last login
                    user.LastLogin = DateTime.Now;
                    _context.SaveChanges();

                    // Redirect
                    if (user.IsAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user != null)
                {
                    // TODO: Implement email sending for password reset
                    // For now, just show a success message
                    TempData["Success"] = "Password reset instructions have been sent to your email.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Email not found");
            }

            return View(model);
        }

        // GET: Account/Profile
        [CustomAuthorize]
        public ActionResult Profile()
        {
            var userId = (int)Session["UserId"];
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Account/OrderHistory
        [CustomAuthorize]
        public ActionResult OrderHistory()
        {
            var userId = (int)Session["UserId"];
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            // Load payments separately for each order
            foreach (var order in orders)
            {
                var payment = _context.Payments.FirstOrDefault(p => p.OrderId == order.OrderId);
                // Store payment in ViewBag or add to ViewData for use in view
            }

            return View(orders);
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
