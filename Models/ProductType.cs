﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCGShop.Models
{
    public class ProductType
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public List<Product> Products { get; set; }
    }
}
