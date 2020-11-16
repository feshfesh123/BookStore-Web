using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWeb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Percent = table.Column<double>(nullable: false),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    DateValid = table.Column<DateTime>(nullable: false),
                    QuantityDiscount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    CreatUser = table.Column<string>(nullable: true),
                    EditUser = table.Column<string>(nullable: true),
                    EditDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(nullable: true),
                    ProducQuantity = table.Column<int>(nullable: false),
                    Tacgia = table.Column<string>(nullable: true),
                    NXB = table.Column<string>(nullable: true),
                    Nhacungcap = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<double>(nullable: false),
                    ProductImage = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(nullable: true),
                    AddressName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    OrderDetailId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DiscountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeID", "Name" },
                values: new object[,]
                {
                    { 1, "Truyện Ngắn" },
                    { 2, "Truyện Dài" },
                    { 3, "Truyện Kind Dị" },
                    { 4, "Tiểu Thuyết" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "NXB", "Nhacungcap", "ProducQuantity", "ProductImage", "ProductName", "ProductPrice", "Tacgia", "TypeId" },
                values: new object[,]
                {
                    { 1, "Dấu chân trên cát là tác phẩm được dịch giả Nguyên Phong phóng tác kể về xã hội Ai Cập thế kỷ thứ XIV trước CN, qua lời kể của nhân vật chính -  Sinuhe.", "NXB Tổng Hợp TPHCM", "FIRST NEW", 20, "dauchantrencat.jpg", "Dấu Chân Trên Cát", 98000.0, "Nguyên Phong", 1 },
                    { 2, "Có đôi khi vào những tháng năm bắt đầu vào đời, giữa vô vàn ngả rẽ và lời khuyên, khi rất nhiều dự định mà thiếu đôi phần định hướng, thì CẢM HỨNG là điều quan trọng để bạn trẻ bắt đầu bước chân đầu tiên trên con đường theo đuổi giấc mơ của mình.", "NXB Trẻ", "NXB Trẻ", 30, "caphecungtony.jpg", "Cà Phê Cùng Tony", 63000.0, "Tony Buổi Sáng", 1 },
                    { 6, "Tôi gửi tình yêu cho mùa hè, nhưng mùa hè không giữ nổi. Mùa hè chỉ biết ra hoa, phượng đỏ sân trường và tiếng ve nỉ non trong lá. Mùa hè ngây ngô, giống như tôi vậy. Nó chẳng làm được những điều tôi ký thác. Nó để Hà Lan đốt tôi, đốt rụi. Trái tim tôi cháy thành tro, rơi vãi trên đường về.", "NXB Trẻ", "NXB Trẻ", 20, "matbiec.jpg", "Mắt Biếc", 88000.0, "Nguyễn Nhật Ánh", 1 },
                    { 3, "OP 100 BEST SELLER - Có một người phạm tội nặng, chết đi không được luân hồi. Nhưng trong lúc linh hồn người này đang mất trí nhớ và trôi nổi vô định về một nơi tối tăm xứng đáng với cậu ta, thì một thiên sứ cánh trắng xuất hiện, giơ tay chặn lại, thông báo rằng cậu vừa trúng phiên xổ số may mắn của thiên đình, nhận được cơ hội tu hành kiêm tái thử thách", "NXB Hội Nhà Vưn", "IPM", 40, "colorful.jpg", "Colorful", 66000.0, "Mori Eto", 2 },
                    { 7, "Cuộc đời chẳng bao giờ đơn giản kể cả đối với một người đàn ông làm công việc dọn rác trên đường phố Prague. Ông ta vừa là một công nhân thuộc đội vệ sinh đường phố vừa là một nhà văn từng có tên tuổi rực rỡ.", "NXB Dân Trí", "Bách Việt", 90, "tinhvarac.jpg", "Tình và Rác", 91000.0, "Ivan Klima", 2 },
                    { 8, " Đây là những nền tảng bí mật của nhân cách - Những tri thức kinh điển giúp bạn hiểu mình sâu sắc hơn bao giờ hết - Những mô hình tâm lí giúp bạn thấu suốt bất kì ai", "NXB Phụ Nữ", "Bách Việt", 20, "anhlaaitoilaai.jpg", "Anh Là Ai, Tôi Là Ai", 88000.0, "Carl Gustave Jung", 2 },
                    { 5, "Được xem là một trong những sự kiện văn chương được chờ đợi nhất, Hannibal và những ngày run rẩy bắt đầu mang người đọc vào cung điện ký ức của một kẻ ăn thịt người, tạo dựng nên một bức chân dung ớn lạnh của tội ác đang âm thầm sinh sôi – một thành công của thể loại kinh dị tâm lý", "NXB Hội Nhà Văn", "Nhã Nam", 10, "hannibal.jpg", "Hannibal", 117000.0, "Thomas Harris", 3 },
                    { 4, "Đó là thứ duy nhất có thể mang theo. Vào đúng khi bạn nhận ra có bao nhiêu đồ đạc cũng chẳng lấp nổi biển trong lòng.", "NXB Trẻ", "NXB Trẻ", 80, "hanhlyhuvo.jpg", "Hành Lý Hư Vô", 76000.0, "Nguyễn Ngọc Tư", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DiscountId",
                table: "OrderDetails",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
