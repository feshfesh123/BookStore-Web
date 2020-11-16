using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class ProductData
    {
        public List<ProductModel> ProductList { get; set; }
        public static ProductData initData()
        {
            List<ProductModel> products = new List<ProductModel>();
            products.AddRange(new List<ProductModel>
            {
                new ProductModel()
                {
                    ProductName="Dấu Chân Trên Cát",
                    ProducQuantity=20,
                    Tacgia="Nguyên Phong",
                    NXB="NXB Tổng Hợp TPHCM",
                    Nhacungcap="FIRST NEW",
                    ProductPrice=98000,
                    ProductImage="dauchantrencat.jpg"
                },
                new ProductModel()
                {
                    ProductName="Cà Phê Cùng Tony",
                    ProducQuantity=30,
                    Tacgia="Tony Buổi Sáng",
                    NXB="NXB Trẻ",
                    Nhacungcap="NXB Trẻ",
                    ProductPrice=63000,
                    ProductImage="caphecungtony.jpg"
                },
                new ProductModel()
                {
                    ProductName="Colorful",
                    ProducQuantity=40,
                    Tacgia="Mori Eto",
                    NXB="NXB Hội Nhà Vưn",
                    Nhacungcap="IPM",
                    ProductPrice=66000,
                    ProductImage="colorful.jpg"
                },

                new ProductModel()
                {
                    ProductName="Hành Lý Hư Vô",
                    ProducQuantity=80,
                    Tacgia="Nguyễn Ngọc Tư",
                    NXB="NXB Trẻ",
                    Nhacungcap="NXB Trẻ",
                    ProductPrice=76000,
                    ProductImage="hanhlyhuvo.jpg"
                },

                new ProductModel()
                {
                    ProductName="Hannibal",
                    ProducQuantity=10,
                    Tacgia="Thomas Harris",
                    NXB="NXB Hội Nhà Văn",
                    Nhacungcap="Nhã Nam",
                    ProductPrice=117000,
                    ProductImage="hannibal.jpg"
                },

                 new ProductModel()
                {
                    ProductName="Mắt Biếc",
                    ProducQuantity=20,
                    Tacgia="Nguyễn Nhật Ánh",
                    NXB="NXB Trẻ",
                    Nhacungcap="NXB Trẻ",
                    ProductPrice=88000,
                    ProductImage="matbiec.jpg"
                },

                 new ProductModel()
                {
                    ProductName="Tình và Rác",
                    ProducQuantity=90,
                    Tacgia="Ivan Klima",
                    NXB="NXB Dân Trí",
                    Nhacungcap="Bách Việt",
                    ProductPrice=91000,
                    ProductImage="tinhvarac.jpg"
                },

                 new ProductModel()
                {
                    ProductName="Anh Là Ai, Tôi Là Ai",
                    ProducQuantity=20,
                    Tacgia="Carl Gustave Jung",
                    NXB="NXB Phụ Nữ",
                    Nhacungcap="Bách Việt",
                    ProductPrice=88000,
                    ProductImage="anhlaaitoilaai.jpg"
                }
            }); ;
            return new ProductData()
            {
                ProductList = products
            };
        }
    }
}
