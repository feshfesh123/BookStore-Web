using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProducQuantity { get; set; }

        public string Tacgia { get; set; }

        public string NXB { get; set; }

        public string Nhacungcap { get; set; }

        public double ProductPrice { get; set; }

        public string  ProductImage { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public int TypeId { get; set; }

    }
}
