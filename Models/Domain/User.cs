using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.Domain
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set;}

        public DateTime CreatDate { get; set; }

        public string CreatUser { get; set; }

        public string EditUser { get; set; }

        public DateTime EditDate { get; set; }

        public string Status { get; set; }
    }
}
