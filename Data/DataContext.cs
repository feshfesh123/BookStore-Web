using BookStoreWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DataContext() : base () { }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Models.Domain.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Bảng OrderDetail có 2 khóa chính
            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });
            builder.Entity<Models.Domain.Type>().HasData(
               new Models.Domain.Type()
               {
                   TypeID = 1,
                   Name = "Truyện Ngắn"
               },
               new Models.Domain.Type()
               {
                   TypeID = 2,
                   Name = "Truyện Dài"
               },
               new Models.Domain.Type()
               {
                   TypeID = 3,
                   Name = "Truyện Kind Dị"
               },
                new Models.Domain.Type()
                {
                    TypeID = 4,
                    Name= "Tiểu Thuyết"
                });
            builder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    ProductName = "Dấu Chân Trên Cát",
                    ProducQuantity = 20,
                    Tacgia = "Nguyên Phong",
                    NXB = "NXB Tổng Hợp TPHCM",
                    Nhacungcap = "FIRST NEW",
                    ProductPrice = 98000,
                    ProductImage = "dauchantrencat.jpg",
                    Description= "Dấu chân trên cát là tác phẩm được dịch giả Nguyên Phong phóng tác kể về xã hội Ai Cập thế kỷ thứ XIV trước CN, qua lời kể của nhân vật chính -  Sinuhe.",
                    TypeId=1

                },
                new Product()
                {
                    ProductId = 2,
                    ProductName = "Cà Phê Cùng Tony",
                    ProducQuantity = 30,
                    Tacgia = "Tony Buổi Sáng",
                    NXB = "NXB Trẻ",
                    Nhacungcap = "NXB Trẻ",
                    ProductPrice = 63000,
                    ProductImage = "caphecungtony.jpg",
                    Description= "Có đôi khi vào những tháng năm bắt đầu vào đời, giữa vô vàn ngả rẽ và lời khuyên, khi rất nhiều dự định mà thiếu đôi phần định hướng, thì CẢM HỨNG là điều quan trọng để bạn trẻ bắt đầu bước chân đầu tiên trên con đường theo đuổi giấc mơ của mình.",
                    TypeId = 1
                },
                new Product()
                {
                    ProductId = 3,
                    ProductName = "Colorful",
                    ProducQuantity = 40,
                    Tacgia = "Mori Eto",
                    NXB = "NXB Hội Nhà Vưn",
                    Nhacungcap = "IPM",
                    ProductPrice = 66000,
                    ProductImage = "colorful.jpg",
                    Description= "OP 100 BEST SELLER - Có một người phạm tội nặng, chết đi không được luân hồi. Nhưng trong lúc linh hồn người này đang mất trí nhớ và trôi nổi vô định về một nơi tối tăm xứng đáng với cậu ta, thì một thiên sứ cánh trắng xuất hiện, giơ tay chặn lại, thông báo rằng cậu vừa trúng phiên xổ số may mắn của thiên đình, nhận được cơ hội tu hành kiêm tái thử thách",
                    TypeId = 2
                },

                new Product()
                {
                    ProductId = 4,
                    ProductName = "Hành Lý Hư Vô",
                    ProducQuantity = 80,
                    Tacgia = "Nguyễn Ngọc Tư",
                    NXB = "NXB Trẻ",
                    Nhacungcap = "NXB Trẻ",
                    ProductPrice = 76000,
                    ProductImage = "hanhlyhuvo.jpg",
                    Description= "Đó là thứ duy nhất có thể mang theo. Vào đúng khi bạn nhận ra có bao nhiêu đồ đạc cũng chẳng lấp nổi biển trong lòng.",
                    TypeId = 4
                },

                new Product()
                {
                    ProductId = 5,
                    ProductName = "Hannibal",
                    ProducQuantity = 10,
                    Tacgia = "Thomas Harris",
                    NXB = "NXB Hội Nhà Văn",
                    Nhacungcap = "Nhã Nam",
                    ProductPrice = 117000,
                    ProductImage = "hannibal.jpg",
                    Description= "Được xem là một trong những sự kiện văn chương được chờ đợi nhất, Hannibal và những ngày run rẩy bắt đầu mang người đọc vào cung điện ký ức của một kẻ ăn thịt người, tạo dựng nên một bức chân dung ớn lạnh của tội ác đang âm thầm sinh sôi – một thành công của thể loại kinh dị tâm lý",
                    TypeId = 3
                },

                 new Product()
                 {
                     ProductId = 6,
                     ProductName = "Mắt Biếc",
                     ProducQuantity = 20,
                     Tacgia = "Nguyễn Nhật Ánh",
                     NXB = "NXB Trẻ",
                     Nhacungcap = "NXB Trẻ",
                     ProductPrice = 88000,
                     ProductImage = "matbiec.jpg",
                     Description= "Tôi gửi tình yêu cho mùa hè, nhưng mùa hè không giữ nổi. Mùa hè chỉ biết ra hoa, phượng đỏ sân trường và tiếng ve nỉ non trong lá. Mùa hè ngây ngô, giống như tôi vậy. Nó chẳng làm được những điều tôi ký thác. Nó để Hà Lan đốt tôi, đốt rụi. Trái tim tôi cháy thành tro, rơi vãi trên đường về.",
                     TypeId = 1
                 },

                 new Product()
                 {
                     ProductId = 7,
                     ProductName = "Tình và Rác",
                     ProducQuantity = 90,
                     Tacgia = "Ivan Klima",
                     NXB = "NXB Dân Trí",
                     Nhacungcap = "Bách Việt",
                     ProductPrice = 91000,
                     ProductImage = "tinhvarac.jpg",
                    Description= "Cuộc đời chẳng bao giờ đơn giản kể cả đối với một người đàn ông làm công việc dọn rác trên đường phố Prague. Ông ta vừa là một công nhân thuộc đội vệ sinh đường phố vừa là một nhà văn từng có tên tuổi rực rỡ.",
                     TypeId = 2

                 },

                 new Product()
                 {
                     ProductId = 8,
                     ProductName = "Anh Là Ai, Tôi Là Ai",
                     ProducQuantity = 20,
                     Tacgia = "Carl Gustave Jung",
                     NXB = "NXB Phụ Nữ",
                     Nhacungcap = "Bách Việt",
                     ProductPrice = 88000,
                     ProductImage = "anhlaaitoilaai.jpg",
                     Description= " Đây là những nền tảng bí mật của nhân cách - Những tri thức kinh điển giúp bạn hiểu mình sâu sắc hơn bao giờ hết - Những mô hình tâm lí giúp bạn thấu suốt bất kì ai",
                     TypeId = 2
                 }
                );
        }




    }
}
