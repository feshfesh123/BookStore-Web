using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime DateTime { get; set; }

        public Users User { get; set; }

        public int UserId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public double TotalPrice { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; } = "đang giao dịch";
    }
}
