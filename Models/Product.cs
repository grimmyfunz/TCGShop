using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCGShop.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public float Price { get; set; }

        public int ID_ProductType { get; set; }

        public ProductType ProductType { get; set; }

    }
}
