using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Omlajue_Ecommerce.Models
{
    /// <summary>
    /// Represents a user in the system (both customers and admins)
    /// </summary>
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Province { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
