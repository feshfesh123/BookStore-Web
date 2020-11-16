using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class ProductModel
    { 

        public int ProductId { get; set; }

        [DisplayName("Tên Sách")]
        [Required(ErrorMessage = "Not null")]
        public string ProductName { get; set; }


        [DisplayName("Số lượng")]
        [Required]
        [Range(0, 100)]
        public int ProducQuantity { get; set; }

        [DisplayName("Tác Giả")]
        [Required(ErrorMessage = "Not null")]
        public string Tacgia { get; set; }

        [DisplayName("NXB")]
        [Required(ErrorMessage = "Not null")]
        public string NXB { get; set; }

        [DisplayName("Nhà Cung Cấp")]
        [Required(ErrorMessage = "Not null")]
        public string Nhacungcap { get; set; }

        [DisplayName("Giá Tiền")]
        [Required]
        [Range(10000, 2000000)]
        public double ProductPrice { get; set; }

        [DisplayName("Hình Ảnh")]
        public string ProductImage { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        private static int nextId = 1;

        public ProductModel()
        {

            ProductId=nextId;
            nextId++;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }
}
