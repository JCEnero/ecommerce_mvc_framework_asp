using System;

namespace Omlajue_Ecommerce.Helpers
{
    /// <summary>
    /// Helper class for generating unique order numbers
    /// </summary>
    public static class OrderHelper
    {
        /// <summary>
        /// Generates a unique order number
        /// Format: ORD-YYYYMMDD-XXXXX (e.g., ORD-20240115-12345)
        /// </summary>
        public static string GenerateOrderNumber()
        {
            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var randomPart = new Random().Next(10000, 99999);
            return $"ORD-{datePart}-{randomPart}";
        }

        /// <summary>
        /// Generates a unique payment number for GCash/Maya
        /// Format: 09XXXXXXXXX
        /// </summary>
        public static string GeneratePaymentNumber()
        {
            var random = new Random();
            var number = random.Next(100000000, 999999999);
            return $"09{number}";
        }
    }
}
