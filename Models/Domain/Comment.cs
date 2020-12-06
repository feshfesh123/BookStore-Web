using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
