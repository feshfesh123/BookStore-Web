using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Controllers
{
    public class DetailController : Controller
    {
        private readonly DataContext dataContext;

        public DetailController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
             
        [HttpGet ("Detail/(id)/(name)")]
        public IActionResult Index(int id)
        {
            ProductModel productModel = new ProductModel();
            var p = dataContext.Products.Where(m => m.ProductId == id).FirstOrDefault();
            if (p==null)
            {
                return View(id);
            }
            else
            {
                productModel.ProductId = p.ProductId;
                productModel.ProductName = p.ProductName;
                productModel.ProducQuantity = p.ProducQuantity;
                productModel.Tacgia = p.Tacgia;
                productModel.NXB = p.NXB;
                productModel.Nhacungcap = p.Nhacungcap;
                productModel.ProductPrice = p.ProductPrice;
                productModel.ProductImage = p.ProductImage;
                productModel.Description = p.Description;
                productModel.Comments = dataContext.Comments.Include(x => x.User).Where(x => x.ProductId == p.ProductId).OrderByDescending(x => x.CreatedAt).ToList();
            }
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Comment(CommentModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            var entity = new Comment
            {
                UserId = userId.GetValueOrDefault(),
                CreatedAt = DateTime.Now,
                ProductId = model.ProductId,
                Content = model.Content
            };

            dataContext.Comments.Add(entity);
            dataContext.SaveChanges();

            return RedirectToAction("Index", new { id = model.ProductId });
        }
    }
}
