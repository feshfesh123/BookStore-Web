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

namespace BookStoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext dataContext;
        private readonly string _ClientId = "AQgOM5XiOSVpMRjQ0fFVC3Yb52leSRl59H6fEaO9kWGhIBGOpegc_ggLtr-zGV4uKpu0CXGnPWbV_pow";
        private readonly string _SecretKey = "EKzy6wAJj-Smzarq2QmNHKNRuz1GGxgIAiIwpgq5y62rDyv06MSMs7m7QPRA8GXvpvN7jekBGfSGbSs_";
        private readonly double TyGiaUSD = 23300;

        private PayPalHttpClient Client;

        public CartController(DataContext dataContext)
        {
            this.dataContext = dataContext;

            var environment = new SandboxEnvironment(_ClientId, _SecretKey);

            Client = new PayPalHttpClient(environment);
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
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
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
                        Quantity = 1,
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

            //else
            //{
            //    
            //    return RedirectToAction("PaypalPayment", model);
            //}

            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = item.Quantity,
                    Amount = item.Quantity * item.ProductModel.ProductPrice,
                    ProductId = item.ProductModel.ProductId
                };

                order.OrderDetails.Add(orderDetail);

                var product = dataContext.Products.FirstOrDefault(x => x.ProductId == item.ProductModel.ProductId);

                if (product != null)
                {
                    if (product.ProducQuantity < item.Quantity)
                    {
                        return View("Views/Shared/OutOfStock.cshtml", product);
                    }

                    product.ProducQuantity -= item.Quantity;

                    dataContext.Products.Update(product);
                }
            }

            var createdOrder = dataContext.Orders.Add(order);
            dataContext.SaveChanges();

            if (model.PaymentMethod != 0)
            {
                model.OrderId = createdOrder.Entity.OrderId;

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return RedirectToAction("PaypalPayment", model);
            }

            return RedirectToAction("PaymentSuccess");
        }

        public async Task<IActionResult> PayPalPayment(CheckOutModel model)
        {
            var order = dataContext.Orders.FirstOrDefault(x => x.OrderId == model.OrderId);

            if (order == null) return NotFound();

            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };

            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");

            HttpContext.Session.Remove("cart");

            double sale = 1;
            if (CheckDiscount(model.DiscountCode) != -1)
            {
                sale = (100 - CheckDiscount(model.DiscountCode)) / 100;
            }

            var total = cart.Sum(item => Math.Round(item.ProductModel.ProductPrice * sale / TyGiaUSD, 2) * item.Quantity);

            foreach (var item in cart)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.ProductModel.ProductName,
                    Currency = "USD",
                    Price = Math.Round(item.ProductModel.ProductPrice * sale / TyGiaUSD, 2).ToString(),
                    Quantity = item.Quantity.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }
            #endregion

            var paypalOrderId = DateTime.Now.Ticks.ToString();
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Id = paypalOrderId,
                Intent = "SALE",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString(),
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId,

                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Cart/PaymentFail",
                    ReturnUrl = $"{hostname}/Cart/PaymentSuccess"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await Client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                order.PaypalId = result.Id;

                dataContext.Orders.Update(order);

                dataContext.SaveChanges();

                return Redirect(paypalRedirectUrl);
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

        public async Task<IActionResult> PaymentSuccess(string paymentId, string payerId)
        {
            if (paymentId != null && payerId != null)
            {
                var order = dataContext.Orders.FirstOrDefault(x => x.PaypalId == paymentId);

                PaymentExecuteRequest request = new PaymentExecuteRequest(order.PaypalId);

                request.RequestBody(new PaymentExecution()
                {
                    PayerId = payerId
                });

                var response = await Client.Execute(request);

                Payment result = response.Result<Payment>();

                if (result.State.ToLower() == "approved")
                {
                    order.IsCheckout = true;

                    dataContext.Orders.Update(order);

                    dataContext.SaveChanges();

                    return View();
                }
                else
                {
                    return RedirectToAction("PaymentFail");
                }
            }

            return View();
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

