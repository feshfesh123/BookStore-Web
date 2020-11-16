using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStoreWeb.Controllers
{
    public class ManageUserController : BaseController
    {
        DataContext data;
        public ManageUserController(DataContext dataContext)
        {
            this.data = dataContext;
        }
        public IActionResult Index()
        {
            var list = data.Users.FromSqlRaw<Users>(@"select * from Users u where u.Status = 'Active' and u.UserId > 1").ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users user)
        {
            using (var dbtran = data.Database.BeginTransaction())
            {
                try
                {
                    var temp = data.Users.FirstOrDefault(m => m.Email == user.Email);
                    if (temp == null)
                    {
                        if (user.Password == user.ConfirmPassword)
                        {
                            user.Status = "Active";
                            user.CreatUser = "Admin";
                            user.CreatDate = DateTime.Now;
                            user.EditUser = "Admin";
                            user.EditDate = DateTime.Now;

                            data.Users.Add(user);
                            data.SaveChanges();
                            dbtran.Commit();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Password and Confirm password does not match" );
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User exists");
                        dbtran.Rollback();
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    dbtran.Rollback();
                    ModelState.AddModelError("", "" + ex.Message);

                    return View(user);
                }
            }
            return View(user);
        }

        public IActionResult Edit(int? id)
        {
            var editUser = data.Users.FirstOrDefault(m => m.UserId == id);
            return View(editUser);
        }

        public IActionResult EditTheme(int? id)
        {
            var editUser = data.Users.FirstOrDefault(m => m.UserId == id);
            return View(editUser);
        }

        [HttpPost]
        public IActionResult Edit(Users user)
        {
            var editUser = data.Users.FirstOrDefault(m => m.UserId == user.UserId);

            editUser.EditUser = "Admin";
            editUser.EditDate = DateTime.Now;
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;

            data.Entry(editUser).State = EntityState.Modified; //giu nguyen cac thuoc tinh khong thay doi va cap nhat lai thuoc tinh minh sua
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            var detailUser = data.Users.FirstOrDefault(m => m.UserId == id);
            return View(detailUser);
        }

        public IActionResult Delete(int id)
        {
            var deleteRow = data.Users.FirstOrDefault(m => m.UserId == id);
            deleteRow.Status = "Deactive";
            deleteRow.EditUser = "Admin";
            deleteRow.EditDate = DateTime.Now;

            data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
