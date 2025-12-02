using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents a customer order
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public decimal SubTotal { get; set; }

        public decimal ShippingFee { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderStatus { get; set; } // Pending, Processing, Shipped, Completed, Cancelled

        // Billing Information
        [Required]
        [StringLength(100)]
        public string BillingFirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string BillingLastName { get; set; }

        [Required]
        [StringLength(255)]
        public string BillingEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string BillingPhone { get; set; }

        [Required]
        [StringLength(500)]
        public string BillingAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string BillingCity { get; set; }

        [Required]
        [StringLength(100)]
        public string BillingProvince { get; set; }

        [Required]
        [StringLength(20)]
        public string BillingPostalCode { get; set; }

        // Shipping Information
        [Required]
        [StringLength(100)]
        public string ShippingFirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingLastName { get; set; }

        [Required]
        [StringLength(20)]
        public string ShippingPhone { get; set; }

        [Required]
        [StringLength(500)]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingCity { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingProvince { get; set; }

        [Required]
        [StringLength(20)]
        public string ShippingPostalCode { get; set; }

        [StringLength(1000)]
        public string OrderNotes { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        [StringLength(500)]
        public string CancellationReason { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
