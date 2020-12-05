using BookStoreWeb.Helper;
using BookStoreWeb.Models;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Services
{
    public class PaymentService
    {
        private readonly string _ClientId = "AQgOM5XiOSVpMRjQ0fFVC3Yb52leSRl59H6fEaO9kWGhIBGOpegc_ggLtr-zGV4uKpu0CXGnPWbV_pow";
        private readonly string _SecretKey = "EKzy6wAJj-Smzarq2QmNHKNRuz1GGxgIAiIwpgq5y62rDyv06MSMs7m7QPRA8GXvpvN7jekBGfSGbSs_";
        private readonly double TyGiaUSD = 23300;

        private PayPalHttpClient Client;
        public PaymentService()
        {
            var environment = new SandboxEnvironment(_ClientId, _SecretKey);

            Client = new PayPalHttpClient(environment);
        }

        public async Task<PaypalPayment> GetURLPaymentWithPaypal(List<ProductToCart> cart, double sale, string hostname)
        {
            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };

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

            return new PaypalPayment() { 
                RedirectURL = paypalRedirectUrl,
                PaypalId = result.Id
            }; 
        }

        public async Task<bool> ExecutePayment(string paypalId, string payerId)
        {
            PaymentExecuteRequest request = new PaymentExecuteRequest(paypalId);

            request.RequestBody(new PaymentExecution()
            {
                PayerId = payerId
            });

            var response = await Client.Execute(request);

            Payment result = response.Result<Payment>();

            return result.State.ToLower() == "approved";
        }
    }
}
