using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Helper;
using BookStoreWeb.Models;
using BookStoreWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BookStoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext dataContext;

        public CartController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [Route("Index")]
        public IActionResult Index(CheckOutModel model)
        {
            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
            {
                ViewBag.total = cart.Sum(item => item.ProductModel.ProductPrice * item.Quantity);
            }
            return View(model);
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {

            if (SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart") == null)
                {
                // Kiem tra gio hang co null hay ko
                Product p = dataContext.Products.Find(id);  //Lay ra ID cua san pham <3
                List<ProductToCart> cart = new List<ProductToCart>();
                cart.Add(new ProductToCart
                {
                    // Add san pham vo Cart
                    ProductModel = new ProductModel()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductImage = p.ProductImage,
                        ProductPrice = p.ProductPrice,
                    },
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson (HttpContext.Session, "cart", cart);
            }
            else
            {
                //Gio hang ko null thi tang gia tri gio hang len theo tung san pham Add
                List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    Product p = dataContext.Products.Find(id);
                    cart.Add(new ProductToCart
                    {
                        ProductModel = new ProductModel()
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            ProductImage = p.ProductImage,
                            ProductPrice = p.ProductPrice
                        },
                        Quantity=1,
                    });
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductModel.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult CheckOut()
        {
            
            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.ProductModel.ProductPrice * item.Quantity);
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(CheckOutModel model)
        {
            // Kiểm tra nếu chưa đăng nhập, trả về trang login
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            
            // Nếu chưa nhập đủ sđt và địa chỉ thì quay lại
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", model);
            }

            // Tính % còn lại sau khi sale
            double sale = 1;
            if (CheckDiscount(model.DiscountCode) != -1)
            {
                sale = (100 - CheckDiscount(model.DiscountCode)) / 100;
            }

            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            HttpContext.Session.Remove("cart");

            var order = new Order()
            {
                DateTime = DateTime.UtcNow,
                UserId = HttpContext.Session.GetInt32("UserID").GetValueOrDefault(),
                TotalPrice = cart.Sum(item => item.ProductModel.ProductPrice * item.Quantity) * sale,
                Address = model.Address,
                Phone = model.PhoneNumber
            };

            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = item.Quantity,
                    Amount = item.Quantity * item.ProductModel.ProductPrice,
                    ProductId = item.ProductModel.ProductId
                };
                order.OrderDetails.Add(orderDetail);
            }

            dataContext.Orders.Add(order);
            dataContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // Action kiểm tra mã giảm giá hợp lệ
        [HttpPost]
        public double CheckDiscount(string Code)
        {
            var discount = dataContext.Discounts.FirstOrDefault(x => x.Name == Code);
            if (discount != null &&
                discount.QuantityDiscount > 0 &&
                discount.CreatDate <= DateTime.Now &&
                discount.DateValid > DateTime.Now)
            {
                return discount.Percent;
            } 
            return -1;
        }
    }

}

