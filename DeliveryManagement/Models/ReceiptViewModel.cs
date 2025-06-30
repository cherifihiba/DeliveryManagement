using System;

namespace DeliveryManagement.Models
{
    public class ReceiptViewModel
    {
        // === Order Details ===
        public int OrderId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime PrintDate { get; set; }

        // === Customer Info ===
        public string CustomerName { get; set; }
        public string Address { get; set; }

        // === Product / Delivery Info ===
        public string Product { get; set; }
        public DateTime DeliveryDate { get; set; }

        // === Deliverer Info ===
        public string DelivererName { get; set; }
        public string DelivererPhone { get; set; }

        // === Company Info ===
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }

        public ReceiptViewModel()
        {
            SetDefaults();
        }

        public ReceiptViewModel(
            int orderId,
            string customerName,
            string address,
            string product,
            DateTime deliveryDate,
            string delivererName,
            string delivererPhone)
        {
            OrderId = orderId;
            CustomerName = customerName;
            Address = address;
            Product = product;
            DeliveryDate = deliveryDate;
            DelivererName = delivererName;
            DelivererPhone = delivererPhone;

            SetDefaults();
        }

        private void SetDefaults()
        {
            PrintDate = DateTime.Now;
            ReceiptNumber = "DR-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            CompanyName = "DeliveryManagement";
            CompanyLogo = "/images/logo.png"; // You can change this path if needed
        }
    }
}
