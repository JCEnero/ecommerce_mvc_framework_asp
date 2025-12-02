using System.ComponentModel.DataAnnotations;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for checkout process
    /// </summary>
    public class CheckoutViewModel
    {
        // Billing Information
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string BillingFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string BillingLastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string BillingEmail { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string BillingPhone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string BillingCity { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [Display(Name = "Province")]
        public string BillingProvince { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Display(Name = "Postal Code")]
        public string BillingPostalCode { get; set; }

        // Shipping Information
        [Display(Name = "Ship to different address")]
        public bool ShipToDifferentAddress { get; set; }

        [Display(Name = "First Name")]
        public string ShippingFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string ShippingLastName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string ShippingPhone { get; set; }

        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Display(Name = "Province")]
        public string ShippingProvince { get; set; }

        [Display(Name = "Postal Code")]
        public string ShippingPostalCode { get; set; }

        [Display(Name = "Order Notes")]
        public string OrderNotes { get; set; }

        // Payment Information
        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        // Cart Summary
        public CartViewModel Cart { get; set; }
    }
}
