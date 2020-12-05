using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class PaypalPayment
    {
        public string RedirectURL { get; set; }
        public string PaypalId { get; set; }
    }
}
