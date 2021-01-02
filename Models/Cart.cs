using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCGShop.Areas.Identity.Data;

namespace TCGShop.Models
{
    public class Cart
    {
        public int ID { get; set; }

        public DateTime CreatedTime { get; set; }

        public int ID_Customer { get; set; }
    }
}
