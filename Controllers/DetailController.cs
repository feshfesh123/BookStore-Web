using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

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

            }
            return View(productModel);
        }
    }
}
