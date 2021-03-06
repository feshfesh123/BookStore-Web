﻿// <auto-generated />
using System;
using BookStoreWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookStoreWeb.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200706161914_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateValid")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Percent")
                        .HasColumnType("float");

                    b.Property<int>("QuantityDiscount")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NXB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nhacungcap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProducQuantity")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<string>("Tacgia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("TypeId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Dấu chân trên cát là tác phẩm được dịch giả Nguyên Phong phóng tác kể về xã hội Ai Cập thế kỷ thứ XIV trước CN, qua lời kể của nhân vật chính -  Sinuhe.",
                            NXB = "NXB Tổng Hợp TPHCM",
                            Nhacungcap = "FIRST NEW",
                            ProducQuantity = 20,
                            ProductImage = "dauchantrencat.jpg",
                            ProductName = "Dấu Chân Trên Cát",
                            ProductPrice = 98000.0,
                            Tacgia = "Nguyên Phong",
                            TypeId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Có đôi khi vào những tháng năm bắt đầu vào đời, giữa vô vàn ngả rẽ và lời khuyên, khi rất nhiều dự định mà thiếu đôi phần định hướng, thì CẢM HỨNG là điều quan trọng để bạn trẻ bắt đầu bước chân đầu tiên trên con đường theo đuổi giấc mơ của mình.",
                            NXB = "NXB Trẻ",
                            Nhacungcap = "NXB Trẻ",
                            ProducQuantity = 30,
                            ProductImage = "caphecungtony.jpg",
                            ProductName = "Cà Phê Cùng Tony",
                            ProductPrice = 63000.0,
                            Tacgia = "Tony Buổi Sáng",
                            TypeId = 1
                        },
                        new
                        {
                            ProductId = 3,
                            Description = "OP 100 BEST SELLER - Có một người phạm tội nặng, chết đi không được luân hồi. Nhưng trong lúc linh hồn người này đang mất trí nhớ và trôi nổi vô định về một nơi tối tăm xứng đáng với cậu ta, thì một thiên sứ cánh trắng xuất hiện, giơ tay chặn lại, thông báo rằng cậu vừa trúng phiên xổ số may mắn của thiên đình, nhận được cơ hội tu hành kiêm tái thử thách",
                            NXB = "NXB Hội Nhà Vưn",
                            Nhacungcap = "IPM",
                            ProducQuantity = 40,
                            ProductImage = "colorful.jpg",
                            ProductName = "Colorful",
                            ProductPrice = 66000.0,
                            Tacgia = "Mori Eto",
                            TypeId = 2
                        },
                        new
                        {
                            ProductId = 4,
                            Description = "Đó là thứ duy nhất có thể mang theo. Vào đúng khi bạn nhận ra có bao nhiêu đồ đạc cũng chẳng lấp nổi biển trong lòng.",
                            NXB = "NXB Trẻ",
                            Nhacungcap = "NXB Trẻ",
                            ProducQuantity = 80,
                            ProductImage = "hanhlyhuvo.jpg",
                            ProductName = "Hành Lý Hư Vô",
                            ProductPrice = 76000.0,
                            Tacgia = "Nguyễn Ngọc Tư",
                            TypeId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            Description = "Được xem là một trong những sự kiện văn chương được chờ đợi nhất, Hannibal và những ngày run rẩy bắt đầu mang người đọc vào cung điện ký ức của một kẻ ăn thịt người, tạo dựng nên một bức chân dung ớn lạnh của tội ác đang âm thầm sinh sôi – một thành công của thể loại kinh dị tâm lý",
                            NXB = "NXB Hội Nhà Văn",
                            Nhacungcap = "Nhã Nam",
                            ProducQuantity = 10,
                            ProductImage = "hannibal.jpg",
                            ProductName = "Hannibal",
                            ProductPrice = 117000.0,
                            Tacgia = "Thomas Harris",
                            TypeId = 3
                        },
                        new
                        {
                            ProductId = 6,
                            Description = "Tôi gửi tình yêu cho mùa hè, nhưng mùa hè không giữ nổi. Mùa hè chỉ biết ra hoa, phượng đỏ sân trường và tiếng ve nỉ non trong lá. Mùa hè ngây ngô, giống như tôi vậy. Nó chẳng làm được những điều tôi ký thác. Nó để Hà Lan đốt tôi, đốt rụi. Trái tim tôi cháy thành tro, rơi vãi trên đường về.",
                            NXB = "NXB Trẻ",
                            Nhacungcap = "NXB Trẻ",
                            ProducQuantity = 20,
                            ProductImage = "matbiec.jpg",
                            ProductName = "Mắt Biếc",
                            ProductPrice = 88000.0,
                            Tacgia = "Nguyễn Nhật Ánh",
                            TypeId = 1
                        },
                        new
                        {
                            ProductId = 7,
                            Description = "Cuộc đời chẳng bao giờ đơn giản kể cả đối với một người đàn ông làm công việc dọn rác trên đường phố Prague. Ông ta vừa là một công nhân thuộc đội vệ sinh đường phố vừa là một nhà văn từng có tên tuổi rực rỡ.",
                            NXB = "NXB Dân Trí",
                            Nhacungcap = "Bách Việt",
                            ProducQuantity = 90,
                            ProductImage = "tinhvarac.jpg",
                            ProductName = "Tình và Rác",
                            ProductPrice = 91000.0,
                            Tacgia = "Ivan Klima",
                            TypeId = 2
                        },
                        new
                        {
                            ProductId = 8,
                            Description = " Đây là những nền tảng bí mật của nhân cách - Những tri thức kinh điển giúp bạn hiểu mình sâu sắc hơn bao giờ hết - Những mô hình tâm lí giúp bạn thấu suốt bất kì ai",
                            NXB = "NXB Phụ Nữ",
                            Nhacungcap = "Bách Việt",
                            ProducQuantity = 20,
                            ProductImage = "anhlaaitoilaai.jpg",
                            ProductName = "Anh Là Ai, Tôi Là Ai",
                            ProductPrice = 88000.0,
                            Tacgia = "Carl Gustave Jung",
                            TypeId = 2
                        });
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Type", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeID");

                    b.ToTable("Types");

                    b.HasData(
                        new
                        {
                            TypeID = 1,
                            Name = "Truyện Ngắn"
                        },
                        new
                        {
                            TypeID = 2,
                            Name = "Truyện Dài"
                        },
                        new
                        {
                            TypeID = 3,
                            Name = "Truyện Kind Dị"
                        },
                        new
                        {
                            TypeID = 4,
                            Name = "Tiểu Thuyết"
                        });
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EditUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Address", b =>
                {
                    b.HasOne("BookStoreWeb.Models.Domain.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Order", b =>
                {
                    b.HasOne("BookStoreWeb.Models.Domain.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.OrderDetail", b =>
                {
                    b.HasOne("BookStoreWeb.Models.Domain.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStoreWeb.Models.Domain.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStoreWeb.Models.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookStoreWeb.Models.Domain.Product", b =>
                {
                    b.HasOne("BookStoreWeb.Models.Domain.Type", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
