using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BookStoreWeb.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DataContext dataContext;
        public AccountsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Users user)
        {
            var temp = dataContext.Users.FirstOrDefault(m => m.Email == user.Email);

            if (temp != null)
            {
                if (temp.Password == user.Password)
                {
                    HttpContext.Session.SetInt32("UserID", temp.UserId);
                    HttpContext.Session.SetString("Email", temp.Email);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            var p = dataContext.Users.FirstOrDefault(p => p.Email == user.Email);
            if (p == null)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    user.CreatDate = DateTime.Now;
                    user.Status = "Active";
                    user.CreatUser = user.FirstName + " " + user.LastName;
                    user.EditDate = DateTime.Now;
                    user.EditUser = user.FirstName + " " + user.LastName;
                    dataContext.Users.Add(user);
                    dataContext.SaveChanges();

                    return RedirectToAction("Login", "Accounts");
                }

                ViewBag.Error = "Password and Confirm Password do not match";
            }
            ViewBag.Error = "User already existed";

            return View(user);
        }

        public ActionResult Logout ()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }
    }
}
