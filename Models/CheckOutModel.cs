using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class CheckOutModel
    {
        [Required(ErrorMessage ="Bạn phải nhập địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        public string DiscountCode { get; set; }
    }
}
