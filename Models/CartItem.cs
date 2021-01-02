using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCGShop.Models
{
    public class CartItem
    {
        public int ID { get; set; }

        public DateTime CreatedTime { get; set; }

        public int Quantity { get; set; }

        public int ID_Cart { get; set; }

        public Cart Cart { get; set; }

        public int ID_Product { get; set; }

        public Product Product { get; set; }
    }
}
