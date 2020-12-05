using BookStoreWeb.Data;
using BookStoreWeb.Models;
using BookStoreWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Services
{
    public class OrderService
    {
        private readonly DataContext _DataContext;
        public OrderService(DataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public List<ProductToCart> AddProductToCart(Product p, List<ProductToCart> cart)
        {
            if (cart == null)
            {
                // Kiem tra gio hang co null hay ko
                cart = new List<ProductToCart>();
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
            }
            else
            {
                //Gio hang ko null thi tang gia tri gio hang len theo tung san pham Add
                int index = isExist(p.ProductId, cart);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
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
            }

            return cart;
        }

        private int isExist(int id, List<ProductToCart> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductModel.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
