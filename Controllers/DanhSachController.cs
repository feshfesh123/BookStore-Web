using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookStoreWeb.Controllers
{
    public class DanhSachController : Controller
    {
        ProductData product;
        DataContext data;

        public DanhSachController(ProductData productData, DataContext dataContext)
        {
            this.product = productData;
            this.data = dataContext;


        }

        //public IActionResult Index()
        //{
        //    List<ProductModel> products = new List<ProductModel>();
        //    List<Product> productss = data.Products.ToList();

        //    foreach (var item in productss)
        //    {
        //        products.Add(new ProductModel()
        //        {
        //            ProductId = item.ProductId,
        //            ProductName = item.ProductName,
        //            ProducQuantity = item.ProducQuantity,
        //            Tacgia = item.Tacgia,
        //            NXB = item.NXB,
        //            Nhacungcap = item.Nhacungcap,
        //            ProductPrice = item.ProductPrice,
        //            ProductImage = item.ProductImage,
        //            Description = item.Description
        //        });
        //    }
        //    return View(products);
        //}

        public IActionResult Index(int? page)
        {
            //List<ProductModel> products = new List<ProductModel>();
            //List<Product> productss = data.Products.ToList();

            //foreach (var item in productss)
            //{
            //    products.Add(new ProductModel()
            //    {
            //        ProductId = item.ProductId,
            //        ProductName = item.ProductName,
            //        ProducQuantity = item.ProducQuantity,
            //        Tacgia = item.Tacgia,
            //        NXB = item.NXB,
            //        Nhacungcap = item.Nhacungcap,
            //        ProductPrice = item.ProductPrice,
            //        ProductImage = item.ProductImage,
            //        Description = item.Description
            //    });
            //}
            //var model = data.Products.OrderByDescending(m=>m.ProductId).ToPagedList(page ?? 1,4);
            //return View(model);

            //if (page == null) page = 1;

          
            //var links = (from l in data.Products
            //             select l).OrderBy(x => x.ProductId);

            //int pageSize = 8;

           
            //int pageNumber = (page ?? 1);
            //ViewBag.products = data.Products.ToList().ToPagedList(pageSize, 1);

            //return View(links.ToPagedList(pageNumber, pageSize));
            var pageNumber = page ?? 1;
            ViewBag.products = data.Products.ToList().ToPagedList(pageNumber, 4);
            return View();

        }

        public IActionResult TruyenNgan()
        {
            List<ProductModel> products = new List<ProductModel>();
            List<Product> productss = data.Products.Include(p=>p.Type).Where(p=>p.TypeId==1).ToList();

            foreach (var item in productss)
            {
                products.Add(new ProductModel()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProducQuantity = item.ProducQuantity,
                    Tacgia = item.Tacgia,
                    NXB = item.NXB,
                    Nhacungcap = item.Nhacungcap,
                    ProductPrice = item.ProductPrice,
                    ProductImage = item.ProductImage,
                    Description = item.Description
                });
            }
            return View(products);
        }

        public IActionResult TruyenDai()
        {
            List<ProductModel> products = new List<ProductModel>();
            List<Product> productss = data.Products.Include(p => p.Type).Where(p => p.TypeId == 2).ToList();

            foreach (var item in productss)
            {
                products.Add(new ProductModel()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProducQuantity = item.ProducQuantity,
                    Tacgia = item.Tacgia,
                    NXB = item.NXB,
                    Nhacungcap = item.Nhacungcap,
                    ProductPrice = item.ProductPrice,
                    ProductImage = item.ProductImage,
                    Description = item.Description
                });
            }
            return View(products);
        }

        public IActionResult TieuThuyet()
        {
            List<ProductModel> products = new List<ProductModel>();
            List<Product> productss = data.Products.Include(p => p.Type).Where(p => p.TypeId == 4).ToList();

            foreach (var item in productss)
            {
                products.Add(new ProductModel()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProducQuantity = item.ProducQuantity,
                    Tacgia = item.Tacgia,
                    NXB = item.NXB,
                    Nhacungcap = item.Nhacungcap,
                    ProductPrice = item.ProductPrice,
                    ProductImage = item.ProductImage,
                    Description = item.Description
                });
            }
            return View(products);
        }
    }
}
