using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents payment information for an order
    /// </summary>
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // GCash, Maya, DebitCard, CashOnDelivery

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } // Pending, Approved, Rejected, Completed

        public decimal Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

        // For GCash/Maya
        [StringLength(100)]
        public string ReferenceNumber { get; set; }

        [StringLength(500)]
        public string ProofOfPaymentUrl { get; set; }

        public DateTime? ProofUploadedAt { get; set; }

        [StringLength(20)]
        public string AssignedPaymentNumber { get; set; }

        // For Debit Card (store minimal info for security)
        [StringLength(4)]
        public string CardLastFourDigits { get; set; }

        [StringLength(100)]
        public string CardHolderName { get; set; }

        [StringLength(500)]
        public string PaymentNotes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
