using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class OrderDetail
    {
        public int Quantity { get; set; }

        public double Amount { get; set; }
        public Discount Discount { get; set; }
        public int? DiscountId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }

        public int ProductId { get; set; }


    }
}
