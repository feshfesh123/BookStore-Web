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
using PayPal.v1.Payments;
using PayPal.Core;
using BraintreeHttp;
using BookStoreWeb.Services;

namespace BookStoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _DataContext;

        private readonly PaymentService _PaymentService;

        private readonly OrderService _OrderService;

        private readonly EmailService _EmailService;

        public CartController(DataContext dataContext, PaymentService paymentService, OrderService orderService, EmailService emailService)
        {
            this._DataContext = dataContext;

            this._PaymentService = paymentService;

            this._OrderService = orderService;

            this._EmailService = emailService;
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
            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");

            Product product = _DataContext.Products.Find(id);  //Lay ra ID cua san pham <3

            cart = _OrderService.AddProductToCart(product, cart);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
           
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

            ViewBag.userId = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
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

            var order = new Models.Domain.Order()
            {
                DateTime = DateTime.UtcNow,
                UserId = HttpContext.Session.GetInt32("UserID").GetValueOrDefault(),
                TotalPrice = cart.Sum(item => item.ProductModel.ProductPrice * item.Quantity) * sale,
                Address = model.Address,
                Phone = model.PhoneNumber
            };

            // Kiem tra xem phuong thuc thanh toan nao
            order.PaymentMethod = model.PaymentMethod == 0 ? "Ship COD" : "Paypal";

            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = item.Quantity,
                    Amount = item.Quantity * item.ProductModel.ProductPrice,
                    ProductId = item.ProductModel.ProductId
                };

                order.OrderDetails.Add(orderDetail);

                var product = _DataContext.Products.FirstOrDefault(x => x.ProductId == item.ProductModel.ProductId);

                if (product != null)
                {
                    if (product.ProducQuantity < item.Quantity)
                    {
                        return View("OutOfStock.cshtml", product);
                    }

                    product.ProducQuantity -= item.Quantity;

                    _DataContext.Products.Update(product);
                }
            }

            var createdOrder = _DataContext.Orders.Add(order);
            _DataContext.SaveChanges();
            
            if (model.PaymentMethod != 0)
            {
                model.OrderId = createdOrder.Entity.OrderId;

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return RedirectToAction("PaypalPayment", model);
            }

            return RedirectToAction("PaymentSuccess", new { orderId = createdOrder.Entity.OrderId.ToString()});
        }

        public async Task<IActionResult> PayPalPayment(CheckOutModel model)
        {
            var order = _DataContext.Orders.FirstOrDefault(x => x.OrderId == model.OrderId);

            if (order == null) return NotFound();

            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");

            HttpContext.Session.Remove("cart");

            double sale = 1;
            if (CheckDiscount(model.DiscountCode) != -1)
            {
                sale = (100 - CheckDiscount(model.DiscountCode)) / 100;
            }

            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            try
            {
                var paypalPayment = await _PaymentService.GetURLPaymentWithPaypal(cart, sale, hostname);

                order.PaypalId = paypalPayment.PaypalId;

                _DataContext.Orders.Update(order);

                _DataContext.SaveChanges();

                return Redirect(paypalPayment.RedirectURL);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Cart/PaymentFail");
            }
        }

        public IActionResult PaymentFail()
        {
            return View();
        }

        public async Task<IActionResult> PaymentSuccess(string paymentId, string payerId, string orderId)
        {
            if (paymentId != null && payerId != null)
            {
                var order = _DataContext.Orders.FirstOrDefault(x => x.PaypalId == paymentId);
                
                var paypalCheckout = await _PaymentService.ExecutePayment(paymentId, payerId);

                if (paypalCheckout)
                {
                    order.IsCheckout = true;

                    _DataContext.Orders.Update(order);

                    await _DataContext.SaveChangesAsync();

                    orderId = order.OrderId.ToString();
                }
                else
                {
                    return RedirectToAction("PaymentFail");
                }
            }

            var userId = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();

            var user = _DataContext.Users.FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                var content = $"CÁM ƠN {user.FirstName} {user.LastName} ĐÃ HÀNG THÀNH CÔNG.<br/> MÃ ĐƠN HÀNG CỦA BẠN LÀ {orderId}.<br/> BẠN CÓ THỂ KIỂM TRA LẠI ĐƠN HÀNG Ở WEBSITE : {HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                await _EmailService.SendMailAsync(user.Email, "ĐẶT HÀNG THÀNH CÔNG", content);
            }

            return View();
        }

        // Action kiểm tra mã giảm giá hợp lệ
        [HttpPost]
        public double CheckDiscount(string Code)
        {
            var discount = _DataContext.Discounts.FirstOrDefault(x => x.Name == Code);
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

