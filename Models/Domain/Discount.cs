using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        public string Name { get; set; }

        public double Percent { get; set; }

        public DateTime CreatDate { get; set; }

        public DateTime DateValid { get; set; }

        public int QuantityDiscount { get; set; }
    }
}
