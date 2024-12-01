using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }
    }
}
