using System.ComponentModel.DataAnnotations;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for forgot password
    /// </summary>
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
