using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
