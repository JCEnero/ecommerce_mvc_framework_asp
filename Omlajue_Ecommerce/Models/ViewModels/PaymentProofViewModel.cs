using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for GCash/Maya payment
    /// </summary>
    public class PaymentProofViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Reference number is required")]
        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Please upload proof of payment")]
        [Display(Name = "Proof of Payment")]
        public HttpPostedFileBase ProofOfPayment { get; set; }

        [Required(ErrorMessage = "Payment date is required")]
        [Display(Name = "Payment Date & Time")]
        public DateTime PaymentDateTime { get; set; }

        public string AssignedPaymentNumber { get; set; }
    }
}
