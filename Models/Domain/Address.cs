using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "")]
        public string Phone { get; set; }

        public string AddressName { get; set; }

        public Users User { get; set; }

        public int UserId { get; set; }
        
    }
}
