﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class ProductToCart
    {
        public ProductModel ProductModel { get; set; }
        public int Quantity { get; set; }
        public ProductData Product { get; set; }
    }
}
