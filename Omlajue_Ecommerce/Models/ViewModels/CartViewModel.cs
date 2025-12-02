using System.Collections.Generic;
using System.Linq;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for shopping cart
    /// </summary>
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Total { get; set; }

        public CartViewModel()
        {
            CartItems = new List<CartItemViewModel>();
        }

        public void CalculateTotals()
        {
            SubTotal = CartItems.Sum(item => item.TotalPrice);
            ShippingFee = SubTotal > 0 ? 150m : 0m; // Flat shipping fee of 150
            Total = SubTotal + ShippingFee;
        }
    }

    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
    }
}
