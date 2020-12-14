using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStoreWeb.Models;
using BookStoreWeb.Data;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        ProductData product;
        DataContext data;

        public HomeController(ProductData productData, DataContext dataContext)
        {
            this.product = productData;
            this.data = dataContext;
        }

        public IActionResult Index()
        {
            List<ProductModel> products = new List<ProductModel>();
            List<Product> productss = data.Products.ToList();

            foreach ( var item in productss)
            {
                products.Add(new ProductModel()
                {
                    ProductId=item.ProductId,
                    ProductName=item.ProductName,
                    ProducQuantity=item.ProducQuantity,
                    Tacgia=item.Tacgia,
                    NXB= item.NXB,
                    Nhacungcap=item.Nhacungcap,
                    ProductPrice=item.ProductPrice,
                    ProductImage=item.ProductImage,
                    Description=item.Description
                });
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SearchItem(string searchname)
        {
            var product = data.Products.FirstOrDefault(m => m.ProductName.Contains(searchname)); //gia tri dau tien hoac mac dinh la null

            return View(product);

            //return RedirectToAction("SearchItem", "Home");
        }

        public IActionResult Sort()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sort(string sort)
        {
            var list = data.Products.ToList();

            if (sort == "Giá giảm dần")
            {
                //list.OrderBy(m => m.ProductPrice);
                list = data.Products.FromSqlRaw<Product>(@"select * from Products p order by p.ProductPrice desc").ToList();
                return View(list);
            }
            if (sort == "Giá tăng dần")
            {
                list = data.Products.FromSqlRaw<Product>(@"select * from Products p order by p.ProductPrice").ToList();
                return View(list);
            }
            if (sort == "Tên A-Z")
            {
                list = data.Products.FromSqlRaw<Product>(@"select * from Products p order by p.ProductName").ToList();
                return View(list);
            }
            if (sort == "Tên Z-A")
            {
                list = data.Products.FromSqlRaw<Product>(@"select * from Products p order by p.ProductName desc").ToList();
                return View(list);
            }

            return View(list);
        }
    }
}
