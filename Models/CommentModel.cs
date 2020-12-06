using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class CommentModel
    {
        public string Content { get; set; }

        public int ProductId { get; set; }
    }
}
