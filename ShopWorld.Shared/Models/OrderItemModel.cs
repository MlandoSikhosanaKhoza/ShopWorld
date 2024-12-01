using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int? QuantityReserved { get; set; }

        public int? QuantityPacked { get; set; }

        public virtual ItemModel Item { get; set; }
    }
}
