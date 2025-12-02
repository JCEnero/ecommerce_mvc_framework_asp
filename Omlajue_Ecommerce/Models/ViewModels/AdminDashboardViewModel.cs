using System;
using System.Collections.Generic;

namespace Omlajue_Ecommerce.Models.ViewModels
{
    /// <summary>
    /// ViewModel for admin dashboard
    /// </summary>
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int PendingOrders { get; set; }
        
        public int TotalOrdersToday { get; set; }
        public int TotalPendingOrders { get; set; }
        public decimal TotalSalesToday { get; set; }
        public decimal TotalSalesThisMonth { get; set; }
        public int LowStockProducts { get; set; }
        public int TotalCustomers { get; set; }
        public int NewCustomersThisMonth { get; set; }
        public int PendingPayments { get; set; }
        
        public List<Order> RecentOrders { get; set; }

        public AdminDashboardViewModel()
        {
            RecentOrders = new List<Order>();
        }
    }
}
