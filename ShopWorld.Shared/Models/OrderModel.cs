using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? DateFulfilled { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid OrderReference { get; set; }

        public decimal VAT { get; set; }

        [Range(0.01,double.MaxValue, ErrorMessage = "Subtotal cannot be submitted as 0")]
        public decimal Subtotal { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Grand total cannot be submitted as 0")]
        public decimal GrandTotal { get; set; }

        public int? WarehouseId { get; set; }

        public string DeliveryAddress { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public EmployeeModel Employee { get; set; }

        public CustomerModel Customer { get; set; }

        public List<OrderItemModel> OrderItem { get; set; } = new List<OrderItemModel>();
    }
}
